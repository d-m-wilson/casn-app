﻿using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class Caller
    {
        public Caller()
        {
            Appointments = new HashSet<Appointment>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public int CiviContactId { get; set; }
        public string CallerIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public bool IsMinor { get; set; }
        public string PreferredLanguage { get; set; }
        public short PreferredContactMethod { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
