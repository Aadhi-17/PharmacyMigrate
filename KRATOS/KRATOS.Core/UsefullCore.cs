using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KRATOS.Core
{
    public class UsefullCore
    {
        public async Task<int> GetTotalEntryToMigrateAsync(string connectionString)
        {
            int countPharmacy = 0;
            int countPharmacyLocation = 0;
            int countPharmacypatient = 0;
            int countPharmacyprescription = 0;
            string QueryTextPharmacy = "SELECT count(PharmacyId) FROM CS_Pharmacy WHERE IsMigrated = 0;";
            string QueryTextPharmacyLoaction = "SELECT count(PharmacyId) FROM CS_PharmacyLocation WHERE IsMigrated = 0;";
            string QueryTextPharmacyPatient = "SELECT count(PharmacyId) FROM CS_PharmacyPrescription WHERE IsMigrated = 0;";
            string QueryTextPharmacyPrescription = "SELECT count(PharmacyId) FROM CS_PharmacyPatient WHERE IsMigrated = 0;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryTextPharmacy, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    countPharmacy = (result != null) ? Convert.ToInt32(result) : 0;

                }
                using (SqlCommand command = new SqlCommand(QueryTextPharmacyLoaction, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    countPharmacyLocation = (result != null) ? Convert.ToInt32(result) : 0;

                }
                using (SqlCommand command = new SqlCommand(QueryTextPharmacyPatient, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    countPharmacypatient = (result != null) ? Convert.ToInt32(result) : 0;

                }
                using (SqlCommand command = new SqlCommand(QueryTextPharmacyPrescription, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    countPharmacyprescription = (result != null) ? Convert.ToInt32(result) : 0;

                }
            }
            return countPharmacy + countPharmacyLocation + countPharmacypatient + countPharmacyprescription;
        }

    }
}
