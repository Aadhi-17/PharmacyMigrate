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
    public class PharmacyLocationCore
    {
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }
        public int error = 0;

        public async Task<int> GetPendingPharmacyLocationToMigrateAsync(string connectionString)
        {
            int count = 0;
            string QueryText = "SELECT count(PharmacyLocationId) FROM CS_PharmacyLocation WHERE IsMigrated = 0;";
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

        public async Task<DataTable> GetAllPendingPharmacyLocationToMigrateAsync(string connectionString, int numberOfdataPerBatch, int offset)
        {
            string query = @"
                SELECT DISTINCT PharmacyLocationId 
                FROM CS_PharmacyLocation 
                WHERE IsMigrated = 0
                ORDER BY PharmacyLocationId
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

        public async Task<DataTable> GetPharmacyLocationInfoAsync(string connectionString, int PharmacylocationId)
        {
            string queryText = @"SELECT 
                                [PharmacyLocationId],
                                [AccountId],
                                [PharmacyId],
                                [LocationId],
                                [CreatedBy],
                                [CreatedOn],
                                [LastUpdatedBy],
                                [LastUpdatedOn],
                                [Version]
                                FROM 
                                [CS_pharmacylocation] WITH(NOLOCK) 
                                WHERE PharmacylocationId = @PharmacylocationId";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(queryText, connection))
            {
                command.Parameters.AddWithValue("@PharmacylocationId", PharmacylocationId);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        public PharmacyLocation GetPharmacyLocation(DataTable dt)
        {
            PharmacyLocation location = new PharmacyLocation
            {
                PharmacyLocationId = Convert.ToInt32(dt.Rows[0]["PharmacyLocationId"]),
                AccountId = Convert.ToInt32(dt.Rows[0]["AccountId"]),
                PharmacyId = Convert.ToInt32(dt.Rows[0]["PharmacyId"]),
                LocationId = Convert.ToInt32(dt.Rows[0]["LocationId"]),
                CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]),
                CreatedOn = DateTime.SpecifyKind(Convert.ToDateTime(dt.Rows[0]["CreatedOn"]), DateTimeKind.Utc),
                LastUpdatedBy = Convert.ToInt32(dt.Rows[0]["LastUpdatedBy"]),
                LastUpdatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                Version = 0
            };
            return location;
        }

        public async Task ExportPharmacyLocationsToMySQLAsync(List<PharmacyLocation> locations, string MySqlconnectionString, string stagingConnectionString)
        {
            using (var connection = new MySqlConnection(MySqlconnectionString))
            {
                await connection.OpenAsync();
                foreach (var location in locations)
                {
                    string query = @"INSERT INTO `pharmacylocation` 
                            (`PharmacyLocationId`, `AccountId`, `PharmacyId`, `LocationId`, 
                             `CreatedBy`, `CreatedOn`, `LastUpdatedBy`, `LastUpdatedOn`, `Version`) 
                             VALUES 
                            (@PharmacyLocationId, @AccountId, @PharmacyId, @LocationId, 
                             @CreatedBy, @CreatedOn, @LastUpdatedBy, @LastUpdatedOn, @Version);";

                    try
                    {
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PharmacyLocationId", location.PharmacyLocationId);
                            command.Parameters.AddWithValue("@AccountId", location.AccountId);
                            command.Parameters.AddWithValue("@PharmacyId", location.PharmacyId);
                            command.Parameters.AddWithValue("@LocationId", location.LocationId);
                            command.Parameters.AddWithValue("@CreatedBy", location.CreatedBy);
                            command.Parameters.AddWithValue("@CreatedOn", location.CreatedOn);
                            command.Parameters.AddWithValue("@LastUpdatedBy", location.LastUpdatedBy);
                            command.Parameters.AddWithValue("@LastUpdatedOn", location.LastUpdatedOn);
                            command.Parameters.AddWithValue("@Version", location.Version);

                            await command.ExecuteNonQueryAsync();
                            await MarkLocationAsMigratedAsync(location.PharmacyLocationId, stagingConnectionString);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        PharmacyProgress.Report(new PharmacyProgress()
                        {
                            LogCategory = LogCategory.Error,
                            Message = $"Error inserting PharmacyLocationId {location.PharmacyLocationId}: {ex.Message}"
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

        public async Task MarkLocationAsMigratedAsync(int pharmacyLocationId, string connectionString)
        {
            string queryText = "UPDATE CS_PharmacyLocation SET IsMigrated = 1 WHERE PharmacyLocationId = @PharmacyLocationId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(queryText, connection))
                {
                    command.Parameters.AddWithValue("@PharmacyLocationId", pharmacyLocationId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }

    }
}
