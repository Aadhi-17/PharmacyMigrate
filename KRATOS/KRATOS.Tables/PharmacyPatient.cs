using System;

namespace KRATOS.Tables
{
    public class PharmacyPatient
    {
        public int AccountId { get; set; }
        public int PatientId { get; set; }
        public int PharmacyId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public long Version { get; set; }

        public PharmacyPatient()
        {
            // Initialize default values
            IsActive = true;
            Version = 0;
        }
    }
}
