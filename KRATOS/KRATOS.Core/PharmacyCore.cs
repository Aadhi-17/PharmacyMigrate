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
    public class PharmacyCore
    {
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }
        public int error = 0;
        public async Task<int> GetPendingPharmacyToMigrateAsync(string connectionString)
        {
            int count = 0;
            string QueryText = "SELECT count(PharmacyId) FROM CS_Pharmacy WHERE IsMigrated = 0;";
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

        public async Task<DataTable> GetAllPendingPharmacyToMigrateAsync(string connectionString, int numberOfdataPerBatch, int offset)
        {
            string query = @"
                SELECT DISTINCT PharmacyId 
                FROM CS_Pharmacy 
                WHERE IsMigrated = 0
                ORDER BY PharmacyId
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

        public async Task<DataTable> GetPharmacyInfoAsync(string connectionString, int PharmacyId)
        {
            string queryText = @"SELECT 
                                 [PharmacyId],
                                 [AccountId],
                                 [PharmacyName],
                                 [AddressLine1],
                                 [AddressLine2],
                                 [City],
                                 [State],
                                 [ZipCode],
                                 [Phone1],
                                 [Phone2],
                                 [Fax],
                                 [Email],
                                 [IsActive],
                                 [CreatedBy],
                                 [CreatedOn],
                                 [LastUpdatedBy],
                                 [LastUpdatedOn],
                                 [Version]
                                 FROM 
                                 [CS_pharmacy] WITH(NOLOCK) 
                                WHERE PharmacyId = @PharmacyId";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryText, connection))
            {
                command.Parameters.AddWithValue("@PharmacyId", PharmacyId);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public Pharmacy GetPharmacy(DataTable dt)
        {
            Pharmacy chart = new Pharmacy
            {
                PharmacyId = Convert.ToInt32(dt.Rows[0]["PharmacyId"]),
                AccountId = Convert.ToInt32(dt.Rows[0]["AccountId"]),
                PharmacyName = dt.Rows[0]["PharmacyName"].ToString(),
                AddressLine1 = dt.Rows[0]["AddressLine1"].ToString(),
                AddressLine2 = dt.Rows[0]["AddressLine2"].ToString(),
                City = dt.Rows[0]["City"].ToString(),
                State = dt.Rows[0]["State"].ToString(),
                ZipCode = dt.Rows[0]["ZipCode"].ToString(),
                Phone1 = dt.Rows[0]["Phone1"].ToString(),
                Phone2 = dt.Rows[0]["Phone2"].ToString(),
                Fax = dt.Rows[0]["Fax"].ToString(),
                Email = dt.Rows[0]["Email"].ToString(),
                IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]),
                CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                CreatedOn = DateTime.SpecifyKind(Convert.ToDateTime(dt.Rows[0]["CreatedOn"]), DateTimeKind.Utc),
                LastUpdatedBy = Convert.ToInt32(dt.Rows[0]["lastUpdatedBy"]),
                LastUpdatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
            };
            chart.Version = 0;
            return chart;
        }

        public async Task MarkAsMigratedAsync(int pharmacyId, string connectionString)
        {
            string queryText = "UPDATE CS_Pharmacy SET IsMigrated = 1 WHERE PharmacyId = @PharmacyId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(queryText, connection))
                {
                    command.Parameters.AddWithValue("@PharmacyId", pharmacyId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }
        public async Task ExportPharmacyToMySQLAsync(List<Pharmacy> pharmacies, string MySQLconnectionString, string StagingconnectionString)
        {
            using (var connection = new MySqlConnection(MySQLconnectionString))
            {
                await connection.OpenAsync();
                foreach (var pharmacy in pharmacies)
                {
                    string query = @"INSERT INTO `pharmacy` 
                                (`PharmacyId`, `AccountId`, `PharmacyName`, `AddressLine1`, `AddressLine2`, 
                                 `City`, `State`, `ZipCode`, `Phone1`, `Phone2`, `Fax`, `Email`, `IsActive`, 
                                 `CreatedBy`, `CreatedOn`, `LastUpdatedBy`, `LastUpdatedOn`, `Version`) 
                                VALUES 
                                (@PharmacyId, @AccountId, @PharmacyName, @AddressLine1, @AddressLine2, 
                                 @City, @State, @ZipCode, @Phone1, @Phone2, @Fax, @Email, @IsActive, 
                                 @CreatedBy, @CreatedOn, @LastUpdatedBy, @LastUpdatedOn, @Version);";
                    try
                    {
                        using (var command = new MySqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@PharmacyId", pharmacy.PharmacyId);
                            command.Parameters.AddWithValue("@AccountId", pharmacy.AccountId);
                            command.Parameters.AddWithValue("@PharmacyName", pharmacy.PharmacyName);
                            command.Parameters.AddWithValue("@AddressLine1", pharmacy.AddressLine1);
                            command.Parameters.AddWithValue("@AddressLine2", pharmacy.AddressLine2);
                            command.Parameters.AddWithValue("@City", pharmacy.City);
                            command.Parameters.AddWithValue("@State", pharmacy.State);
                            command.Parameters.AddWithValue("@ZipCode", pharmacy.ZipCode);
                            command.Parameters.AddWithValue("@Phone1", pharmacy.Phone1);
                            command.Parameters.AddWithValue("@Phone2", pharmacy.Phone2);
                            command.Parameters.AddWithValue("@Fax", pharmacy.Fax);
                            command.Parameters.AddWithValue("@Email", pharmacy.Email);
                            command.Parameters.AddWithValue("@IsActive", pharmacy.IsActive);
                            command.Parameters.AddWithValue("@CreatedBy", pharmacy.CreatedBy);
                            command.Parameters.AddWithValue("@CreatedOn", pharmacy.CreatedOn);
                            command.Parameters.AddWithValue("@LastUpdatedBy", pharmacy.LastUpdatedBy);
                            command.Parameters.AddWithValue("@LastUpdatedOn", pharmacy.LastUpdatedOn);
                            command.Parameters.AddWithValue("@Version", pharmacy.Version);

                            // Execute the query asynchronously (insert each row)
                            await command.ExecuteNonQueryAsync();
                            await MarkAsMigratedAsync(pharmacy.PharmacyId, StagingconnectionString);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        PharmacyProgress.Report(new PharmacyProgress()
                        {
                            LogCategory = LogCategory.Error,
                            Message = $"Error inserting record for PharmacyId {pharmacy.PharmacyId}: {ex.Message}"
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
    }
}
