import MySQLdb
import random

APPOINTMENT_UPDATE="""
update appointment 
 set AppointmentDate = "%s" 
 where id = %d
;
"""

FETCHALL_QUERY="""
select %s from %s 
%s
;
"""

APPT_INSERT="""
insert into appointment
	(DispatcherID,PatientID,ClinicId,PickupLocationVague,DropoffLocationVague,AppointmentDate,AppointmentTypeId)
	values (%s)
;
"""

""" Fake data for DRIVE """
STREETS = [
	"Maple", "Market", "High", "Main", "Crocket", "Buffalo Bayou", "Smith", "Peach Tree"
	]
CITIES=[
	'Houston',
	'Katy',
	'Pasadena',
	'Cypress',
	]
STATES=['TX']

def pick_one(seq):
	return seq[random.randrange(len(seq))]

def connect_to_db():
	db = MySQLdb.Connect(
		host="casnapptest.dmwilson.info",
		user="developers",
		passwd="developers",
		db="casn-app",
#		ssl={'ca':'/usr/local/env/ca-bundle.crt'}    # magic
		)
	return db

def load_table(table_name,fields='*',where=None):
	db = connect_to_db()
	db.query(FETCHALL_QUERY%(fields,table_name,"" if where == None else 'where '+where))
	rows = db.store_result().fetch_row(0,1)
	db.close()
	return rows

def rand_date():
	return "2018-%02d-%02d %02d:%02d:00"%(
		6+random.randrange(7),1+random.randrange(29),
		8+random.randrange(8),15*random.randrange(4))

def rand_vague_loc():
	return '%s, %s'%(pick_one(STREETS),pick_one(CITIES))


def make_appt(db,vol,patient,floc='here',tloc='there'):
	vals = "%d, %d, %d, '%s', '%s', '%s', %d"% (
		vol['Id'],
		patient['Id'],
		1234, #clinic ID
		floc, #from, vague
		tloc, #to, vague
		rand_date(), #appt-date
		random.randrange(4)+1, #appt-type
		)
	db.query(APPT_INSERT%vals)

# test make_appt
def tryone():
 	v = load_table('volunteer')
 	p = load_table('patient')
 	db = connect_to_db()
 	db.begin()
	make_appt(db,v[0],p[0])
	db.commit()
	db.close()


def make_random_appts(count=10):
	dispatchers = load_table('volunteer',where='IsDispatcher = 1')
 	pats = load_table('patient')
 	db = connect_to_db()
 	db.begin()
 	for ii in range(count):
		make_appt(db,
	 		pick_one(dispatchers),
	 		pick_one(pats),
			floc=rand_vague_loc(),
			tloc=rand_vague_loc()
		)
 	db.commit()
 	db.close()

DRIVE_INSERT="""
insert into drive
	(AppointmentID,Direction,
		# DriverId,
		StartAddress, StartCity, StartState, StartPostalCode,
		EndAddress, EndCity, EndState, EndPostalCode
		# Created, Updated
	) values (%d, %d,
		%s,
		%s)
;
"""

def rand_precise_loc():
	street = "%d %s"% (random.randrange(99999)+1, pick_one (STREETS))
	post_code = random.randrange(99999)+1
	return "'%s', '%s', '%s', '%05d' "%(street, pick_one(CITIES), pick_one(STATES), post_code,)

def make_random_drive(db,apptId):
	from_add = rand_precise_loc()
	to_add = rand_precise_loc()
	dirs = random.randrange(10)
	if dirs < 9:		
		db_query(DRIVE_INSERT%(apptId,1,from_add,to_add))
	if dirs > 0:
		db_query(DRIVE_INSERT%(apptId,2,to_add,from_add))


def make_drives():
	appts = load_table('appointment','Id')
	apIds = set()
	for a in appts:
		apIds.add(int(a["Id"]))
	drives = load_table('drive','AppointmentId')
	for d in drives:
		aId = d.get("AppointmentId")
		if aId:
			try:
				apIds.remove(int(aId))
			except:
				pass

 	db = connect_to_db()
 	db.begin()
	for a in apIds: 
		make_random_drive(db,a)
 	db.commit()
 	db.close()

def del_drive(n):
	db = connect_to_db()
	db.begin()
	db.query('delete from drive where id = %d'%n)
	db.commit()
	db.close()


def set_random_appt_dates():
	appts = load_table('appointment') # ,"Id, AppointmentId")
 	db = connect_to_db()
 	db.begin()
 	for a in appts:
 		if not a["AppointmentDate"]:
 			db.query(APPOINTMENT_UPDATE%(rand_date(),a["Id"],))
 	db.commit()
 	db.close()

