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
    public class PharmacyPrescriptionCore
    {
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }
        public int error = 0;

        public async Task<int> GetPendingPharmacyPrescriptionToMigrateAsync(string connectionString)
        {
            int count = 0;
            string QueryText = "SELECT count(prescriptionid) FROM CS_PharmacyPrescription WHERE IsMigrated = 0;";
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

        public async Task<DataTable> GetAllPendingPharmacyPrescriptionToMigrateAsync(string connectionString, int numberOfdataPerBatch, int offset)
        {
            string query = @"
                SELECT DISTINCT prescriptionid
                FROM CS_PharmacyPrescription 
                WHERE IsMigrated = 0
                ORDER BY prescriptionid
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

        public async Task<DataTable> GetPharmacyPrescriptionInfoAsync(string connectionString, int PrescriptionId)
        {
            string queryText = @"
        SELECT 
            [AccountId],
            [PrescriptionId],
            [PharmacyId],
            [IsActive],
            [CreatedBy],
            [CreatedOn],
            [LastUpdatedBy],
            [LastUpdatedOn],
            [Version]
        FROM 
            [CS_pharmacyprescription] WITH(NOLOCK) 
        WHERE 
            PrescriptionId = @PrescriptionId";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryText, connection))
            {
                command.Parameters.AddWithValue("@PrescriptionId", PrescriptionId);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public PharmacyPrescription GetPharmacyPrescription(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            PharmacyPrescription prescription = new PharmacyPrescription
            {
                AccountId = Convert.ToInt32(row["AccountId"]),
                PrescriptionId = Convert.ToInt32(row["PrescriptionId"]),
                PharmacyId = Convert.ToInt32(row["PharmacyId"]),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                CreatedOn = DateTime.SpecifyKind(Convert.ToDateTime(row["CreatedOn"]), DateTimeKind.Utc),
                LastUpdatedBy = Convert.ToInt32(row["LastUpdatedBy"]),
                LastUpdatedOn = DateTime.SpecifyKind(Convert.ToDateTime(row["LastUpdatedOn"]), DateTimeKind.Utc),
                Version = Convert.ToInt64(row["Version"])
            };

            return prescription;
        }
        public async Task ExportPharmacyPrescriptionsToMySQLAsync(List<PharmacyPrescription> prescriptions, string mySqlConnectionString, string stagingConnectionString)
        {
            using (var connection = new MySqlConnection(mySqlConnectionString))
            {
                await connection.OpenAsync();

                foreach (var p in prescriptions)
                {
                    string query = @"INSERT INTO `pharmacyprescription` 
                (`AccountId`, `PrescriptionId`, `PharmacyId`, `IsActive`, 
                 `CreatedBy`, `CreatedOn`, `LastUpdatedBy`, `LastUpdatedOn`, `Version`) 
                VALUES 
                (@AccountId, @PrescriptionId, @PharmacyId, @IsActive, 
                 @CreatedBy, @CreatedOn, @LastUpdatedBy, @LastUpdatedOn, @Version);";

                    try
                    {
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AccountId", p.AccountId);
                            command.Parameters.AddWithValue("@PrescriptionId", p.PrescriptionId);
                            command.Parameters.AddWithValue("@PharmacyId", p.PharmacyId);
                            command.Parameters.AddWithValue("@IsActive", p.IsActive);
                            command.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
                            command.Parameters.AddWithValue("@CreatedOn", p.CreatedOn);
                            command.Parameters.AddWithValue("@LastUpdatedBy", p.LastUpdatedBy);
                            command.Parameters.AddWithValue("@LastUpdatedOn", p.LastUpdatedOn);
                            command.Parameters.AddWithValue("@Version", p.Version);

                            await command.ExecuteNonQueryAsync();

                            // You might want to mark the prescription or related record as migrated
                            await MarkPrescriptionAsMigratedAsync(p.AccountId, p.PrescriptionId, stagingConnectionString);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        PharmacyProgress.Report(new PharmacyProgress()
                        {
                            LogCategory = LogCategory.Error,
                            Message = $"Error inserting record for PrescriptionId {p.PrescriptionId}: {ex.Message}"
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

        public async Task MarkPrescriptionAsMigratedAsync(int accountId, int prescriptionId, string connectionString)
        {
            string queryText = "UPDATE CS_Pharmacyprescription SET IsMigrated = 1 WHERE PrescriptionId = @PrescriptionId AND AccountId = @AccountId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(queryText, connection))
                {
                    command.Parameters.AddWithValue("@PrescriptionId", prescriptionId);
                    command.Parameters.AddWithValue("@AccountId", accountId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }


    }
}
