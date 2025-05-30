using System;

namespace KRATOS.Tables
{
    public class PharmacyLocation
    {
        public int PharmacyLocationId { get; set; }
        public int AccountId { get; set; }
        public int PharmacyId { get; set; }
        public int LocationId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public long Version { get; set; }

        public PharmacyLocation()
        {
            LocationId = 0;
            Version = 0;
        }
    }
}
