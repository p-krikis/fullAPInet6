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
        public async Task<int> SaveListData(string jsonData)
        {
            ListParsingModels jsonDataContent = JsonConvert.DeserializeObject<ListParsingModels>(jsonData);
            string concNameData = String.Join(",", jsonDataContent.NameData); //string[] nameData = concNameData.Split(','); --- to split the concatenatedStrings
            string concDescData = String.Join(",", jsonDataContent.DescData); //string[] descData = concDescData.Split(',');
            int id;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO [dbo].[listDataNew] (nameData, descData, userid, listName, timeCreated) VALUES (@nameData, @descData, @userid, @listName, @timeCreated)";
                id = await connection.QuerySingleOrDefaultAsync<int>(sqlQuery, new {nameData = concNameData, descData = concDescData, userid = jsonDataContent.UserId, listName = jsonDataContent.ListName, timeCreated = DateTime.Now});
            }
            return id;
        }
        public async Task<List<(string listName, DateTime timeCreated)>> LoadAllLists(string targetUserId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT listName, timeCreated FROM [dbo].[listDataNew] WHERE userid = @userid", connection))
                {
                    command.Parameters.AddWithValue("@userid", targetUserId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var responseList = new List<(string  listName, DateTime timeCreated)>();
                        while (await reader.ReadAsync())
                        {
                            responseList.Add((reader.GetString(0), reader.GetDateTime(1)));
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
                using (var command = new SqlCommand("SELECT nameData, descData FROM [dbo].[listDataNew] WHERE userid = @userid AND listName = @listName", connection))
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
                using (var command = new SqlCommand("DELETE FROM [dbo].[listDataNew] WHERE userid = @userid AND listName = @listName", connection))
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
