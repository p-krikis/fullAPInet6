using Dapper;
using fullAPInet6.Models;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using System.Data.SqlClient;

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
            ListParsingModels jsonDataContent = JsonConvert.DeserializeObject<ListParsingModels>(jsonData);
            //string concNameData = String.Join(",", jsonDataContent.NameData); //string[] nameData = concNameData.Split(','); --- to split the concatenatedStrings
            //string concDescData = String.Join(",", jsonDataContent.DescData); //string[] descData = concDescData.Split(',');
            //string jsonTimeCreated = JsonConvert.SerializeObject(DateTime.Now.ToString());
            //string jsonUserId = JsonConvert.SerializeObject(jsonDataContent.UserId);
            //string jsonListName = JsonConvert.SerializeObject(jsonDataContent.ListName);
            //string jsonNameData = JsonConvert.SerializeObject(concNameData);
            //string jsonDescData = JsonConvert.SerializeObject(concDescData);
            int id;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO [dbo].[jsonList] (userid, listName, jsonString, timeCreated) VALUES (@userid, @listName, @jsonString, @timeCreated)";
                id = await connection.QuerySingleOrDefaultAsync<int>(sqlQuery, new { userid = userId, listName = listName, jsonString = jsonData, timeCreated = DateTime.Now });
                //{ nameData = jsonNameData, descData = jsonDescData, userid = jsonUserId, listName = jsonListName, timeCreated = jsonTimeCreated });
            }
            return id;
        }

        public async Task<List<ReturnType>> LoadAllLists(string targetUserId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT listName, timeCreated FROM [dbo].[jsonList] WHERE userid = @userid", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<ReturnType> responseList = new List<ReturnType>();

                        while (await reader.ReadAsync())
                        {
                            responseList.Add(new ReturnType()
                            {
                                listName = reader.GetString(0),
                                timeCreated = reader.GetDateTime(1).ToString()
                            });
                        }
                        return responseList;
                    }
                }
            }
        }

        public async Task<(string[]? nameData, string[]? descData)> LoadSingleList(string targetUserId, string targetListName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT nameData, descData FROM [dbo].[jsonList] WHERE userid = @userid AND listName = @listName", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);
                    command.Parameters.AddWithValue("@listName", targetListName);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        try
                        {
                            if (await reader.ReadAsync())
                            {
                                string concNameData = reader.GetString(0);
                                string concDescData = reader.GetString(1);
                                string[] splitNameData = concNameData.Split(',');
                                string[] splitDescData = concDescData.Split(',');
                                return (splitNameData, splitDescData);
                            }
                            else
                            {
                                return (null, null);
                            }
                        }
                        catch
                        {
                            return (null, null);
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