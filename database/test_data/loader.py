import MySQLdb

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

import random

def make_random_appts(count=10):
	vols = load_table('volunteer',where='IsDispatcher = 1')
	for v in vols:
		if v["IsDispatcher"] == '\x00':
			vols.remove(v)
 	pats = load_table('patient')
 	db = connect_to_db()
 	db.begin()
 	for ii in range(count):
	 	v = vols[random.randrange(len(vols))] 
	 	p = pats[random.randrange(len(pats))]
 		make_appt(db,v,p)
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

