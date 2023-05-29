using fullAPInet6.Models;
using Newtonsoft.Json;
using Dapper;
using System.Data.SqlClient;

namespace fullAPInet6.Services
{
    public class UserInfoHandlingService
    {
        private readonly IConfiguration _configuration;

        public UserInfoHandlingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveUserInfo (string jsonString)
        {
            UserParsingModels parsedData = JsonConvert.DeserializeObject<UserParsingModels>(jsonString);
            int userListId;
            string bob= parsedData.Username;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO [dbo].[userInfoList] (Username, Email, Password, UserId) VALUES (@username, @email, @password, @userid)";
                userListId = await connection.QuerySingleOrDefaultAsync<int>(sqlQuery, new { username = parsedData.Username, email = parsedData.Email, password = parsedData.Password, userid = parsedData.UserId });

            }
            return userListId;
        }
        public async Task<string> AuthUser(string jsonString)
        {
            UserParsingModels parsedData = JsonConvert.DeserializeObject<UserParsingModels>(jsonString);
            string email = parsedData.Email;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT password, userid FROM [dbo].[userInfoList] WHERE email = @email", connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    var result = await command.ExecuteReaderAsync();
                    try
                    {
                        if (await result.ReadAsync())
                        {
                            string fetchedPassword = result.GetString(0);
                            if (parsedData.Password == fetchedPassword)
                            {
                                string userId = result.GetString(1);
                                return userId;
                            }
                            else
                            {
                                return null;
                            }
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
        public async Task DeleteUserByListId(int userListId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (var command = new SqlCommand("DELETE FROM [dbo].[userInfoList] WHERE userListId = @userListId", connection))
                {
                    command.Parameters.AddWithValue("@userListId", userListId);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
