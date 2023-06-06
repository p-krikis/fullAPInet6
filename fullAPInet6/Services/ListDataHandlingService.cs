using Dapper;
using fullAPInet6.Models;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace fullAPInet6.Services
{
    public class ListDataHandlingService
    {
        private readonly IConfiguration _configuration;

        public ListDataHandlingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveListData(string listName, string userId, string jsonData)
        {
            int id;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO [dbo].[jsonList] (userid, listName, jsonString, timeCreated) VALUES (@userid, @listName, @jsonString, @timeCreated)";
                id = await connection.QuerySingleOrDefaultAsync<int>(sqlQuery, new { userid = userId, listName = listName, jsonString = jsonData, timeCreated = DateTime.Now });
            }
            return id;
        }

        public async Task<List<ReturnType>> LoadAllLists(string targetUserId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT listid, listName, timeCreated FROM [dbo].[jsonList] WHERE userid = @userid", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<ReturnType> responseList = new List<ReturnType>();

                        while (await reader.ReadAsync())
                        {
                            responseList.Add(new ReturnType()
                            {
                                listid = reader.GetInt32(0),
                                listName = reader.GetString(1),
                                timeCreated = reader.GetDateTime(2).ToString()
                            });
                        }
                        return responseList;
                    }
                }
            }
        }

        public async Task<string> LoadSingleList(string targetUserId, string targetListName)
        {
            var test = "awdawd";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT jsonString FROM [dbo].[jsonList] WHERE userid = @userid AND listName = @listName", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);
                    command.Parameters.AddWithValue("@listName", targetListName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        try
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetString(0);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task DeleteSpecificList(string targetUserId, string targetListName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (var command = new SqlCommand("DELETE FROM [dbo].[jsonList] WHERE userid = @userid AND listName = @listName", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);
                    command.Parameters.AddWithValue("@listName", targetListName);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}