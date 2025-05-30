using System;

namespace KRATOS.Tables
{
    public class PharmacyPrescription
    {
        public int AccountId { get; set; }
        public int PrescriptionId { get; set; }
        public int PharmacyId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public long Version { get; set; }

        public PharmacyPrescription()
        {
            IsActive = true;
            Version = 0;
        }
    }
}
