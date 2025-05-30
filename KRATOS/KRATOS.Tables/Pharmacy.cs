using System;

namespace KRATOS.Tables
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }
        public int AccountId { get; set; }
        public string PharmacyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public long Version { get; set; } = 0;

        public Pharmacy()
        {
            // Initialize optional strings to empty to avoid null issues
            AddressLine2 = string.Empty;
            City = string.Empty;
            Phone1 = string.Empty;
            Phone2 = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;

            // Initialize default values
            IsActive = true;
            Version = 0;
        }
    }
}
