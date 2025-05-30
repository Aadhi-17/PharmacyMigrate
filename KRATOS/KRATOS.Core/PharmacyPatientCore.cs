using KRATOS.Progress;
using KRATOS.Tables;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KRATOS.Core
{
    public class PharmacyPatientCore
    {
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }
        public int error = 0;

        public async Task<int> GetPendingPharmacyPatientToMigrateAsync(string connectionString)
        {
            int count = 0;
            string QueryText = "SELECT count(patientid) FROM CS_PharmacyPatient WHERE IsMigrated = 0;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryText, connection))
                {
                    object result = await command.ExecuteScalarAsync();
                    count = (result != null) ? Convert.ToInt32(result) : 0;

                }
            }
            return count;
        }

        public async Task<DataTable> GetAllPendingPharmacyPatientToMigrateAsync(string connectionString, int numberOfdataPerBatch, int offset)
        {
            string query = @"
                SELECT DISTINCT PatientId,Accountid 
                FROM CS_PharmacyPatient 
                WHERE IsMigrated = 0
                ORDER BY PatientId,Accountid
                OFFSET @offset ROWS FETCH NEXT @dataPerBatch ROWS ONLY;
            ";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters safely
                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@dataPerBatch", numberOfdataPerBatch);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public async Task<DataTable> GetPharmacyPatientInfoAsync(string connectionString, int patientid)
        {
            string queryText = @"SELECT 
                         [AccountId],
                         [PharmacyId],
                         [PatientId],
                         [IsActive],
                         [CreatedBy],
                         [CreatedOn],
                         [LastUpdatedBy],
                         [LastUpdatedOn],
                         [Version]
                         FROM 
                         [CS_pharmacypatient] WITH(NOLOCK) 
                         WHERE Patientid = @patientid";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryText, connection))
            {
                command.Parameters.AddWithValue("@PatientId", patientid);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public PharmacyPatient GetPharmacyPatient(DataTable dt)
        {
            PharmacyPatient patient = new PharmacyPatient
            {
                AccountId = Convert.ToInt32(dt.Rows[0]["AccountId"]),
                PharmacyId = Convert.ToInt32(dt.Rows[0]["PharmacyId"]),
                PatientId = Convert.ToInt32(dt.Rows[0]["PatientId"]),
                IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]),
                CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                CreatedOn = DateTime.SpecifyKind(Convert.ToDateTime(dt.Rows[0]["CreatedOn"]), DateTimeKind.Utc),
                LastUpdatedBy = Convert.ToInt32(dt.Rows[0]["LastUpdatedBy"]),
                LastUpdatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                Version = 0
            };
            return patient;
        }

        public async Task ExportPharmacyPatientsToMySQLAsync(List<PharmacyPatient> patients, string mySqlConnectionString, string stagingConnectionString)
        {
            using (var connection = new MySqlConnection(mySqlConnectionString))
            {
                await connection.OpenAsync();

                foreach (var patient in patients)
                {
                    string query = @"INSERT INTO `pharmacypatient` 
                            (`AccountId`, `PharmacyId`, `PatientId`, `IsActive`,`CreatedBy`, `CreatedOn`, `LastUpdatedBy`, `LastUpdatedOn`, `Version`) 
                             VALUES 
                            (@AccountId, @PharmacyId, @PatientId,@IsActive, 
                             @CreatedBy, @CreatedOn, @LastUpdatedBy, @LastUpdatedOn, @Version);";

                    try
                    {
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AccountId", patient.AccountId);
                            command.Parameters.AddWithValue("@PharmacyId", patient.PharmacyId);
                            command.Parameters.AddWithValue("@PatientId", patient.PatientId);
                            command.Parameters.AddWithValue("@IsActive", patient.IsActive);
                            command.Parameters.AddWithValue("@CreatedBy", patient.CreatedBy);
                            command.Parameters.AddWithValue("@CreatedOn", patient.CreatedOn);
                            command.Parameters.AddWithValue("@LastUpdatedBy", patient.LastUpdatedBy);
                            command.Parameters.AddWithValue("@LastUpdatedOn", patient.LastUpdatedOn);
                            command.Parameters.AddWithValue("@Version", patient.Version);

                            await command.ExecuteNonQueryAsync();
                            await MarkPatientAsMigratedAsync(patient.PatientId, stagingConnectionString);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        PharmacyProgress.Report(new PharmacyProgress()
                        {
                            LogCategory = LogCategory.Error,
                            Message = $"Error inserting PatientId {patient.PatientId}: {ex.Message}"
                        });
                        error = 1;
                    }
                    catch (Exception ex)
                    {
                        PharmacyProgress.Report(new PharmacyProgress()
                        {
                            LogCategory = LogCategory.Error,
                            Message = $"An unexpected error occurred: {ex.Message}"
                        });
                        error = 1;
                    }
                }
            }
        }

        public async Task MarkPatientAsMigratedAsync(int patientId, string connectionString)
        {
            string queryText = "UPDATE CS_PharmacyPatient SET IsMigrated = 1 WHERE PatientId = @PatientId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(queryText, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patientId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }
    }
}
