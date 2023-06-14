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
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sqlQuery = "INSERT INTO [dbo].[userInfoFull] (Username, Email, Password, UserId, DisplayName, Role) VALUES (@username, @email, @password, @userid, @displayname, @role)";
                userListId = await connection.QuerySingleOrDefaultAsync<int>(sqlQuery, new { username = parsedData.Username, email = parsedData.Email, password = parsedData.Password, userid = parsedData.UserId, displayName = parsedData.DisplayName, role = parsedData.Role });

            }
            return userListId;
        }
        //public async Task<string> AuthUser(string jsonString)
        //{
        //    UserParsingModels parsedData = JsonConvert.DeserializeObject<UserParsingModels>(jsonString);
        //    string email = parsedData.Email;
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();
        //        using (var command = new SqlCommand("SELECT password, userid FROM [dbo].[userInfoFull] WHERE email = @email", connection))
        //        {
        //            command.Parameters.AddWithValue("@email", email);
        //            var result = await command.ExecuteReaderAsync();
        //            try
        //            {
        //                if (await result.ReadAsync())
        //                {
        //                    string fetchedPassword = result.GetString(0);
        //                    if (parsedData.Password == fetchedPassword)
        //                    {
        //                        string userId = result.GetString(1);
        //                        return userId;
        //                    }
        //                    else
        //                    {
        //                        return null;
        //                    }
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }
        //            catch 
        //            { 
        //                return null;
        //            }
        //        }
        //    }
        //}
        //public async Task<List<UserInfoResponse>> UserInfo(string targetUserId)
        //{
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();
        //        using (var command = new SqlCommand("SELECT email, username, displayName, role FROM [dbo].[userInfoFull] WHERE userid = @userid", connection))
        //        {
        //            command.Parameters.AddWithValue("@userid", targetUserId);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                List<UserInfoResponse> responseList = new List<UserInfoResponse>();

        //                while (await reader.ReadAsync())
        //                {
        //                    responseList.Add(new UserInfoResponse()
        //                    {
        //                        email = reader.GetString(0),
        //                        username = reader.GetString(1),
        //                        displayName = reader.GetString(2),
        //                        role = reader.GetString(3)
        //                    });
        //                }
        //                return responseList;
        //            }
        //        }
        //    }
        //}
        //public async Task<int> UpdateUserInfo(string targetUserId, string jsonContent)
        //{
        //    UpdatedInfo updatedInfo = JsonConvert.DeserializeObject<UpdatedInfo>(jsonContent);
        //    int updUserListId;

        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        await connection.OpenAsync();
        //        var sqlQuery = "UPDATE userInfoFull SET Username = @username, Email = @Email, DisplayName = @DisplayName, Role = @Role WHERE userid = @userid";
        //        updUserListId = await connection.ExecuteAsync(sqlQuery, new { username = updatedInfo.Username, email = updatedInfo.Email, displayName = updatedInfo.DisplayName, role = updatedInfo.Role, userid = updatedInfo.UserId });
        //    }

        //    return updUserListId;
        //}
        //public async Task DeleteUserByListId(string userid)
        //{
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        using (var command = new SqlCommand("DELETE FROM [dbo].[userInfoFull] WHERE userid = @userid", connection))
        //        {
        //            command.Parameters.AddWithValue("@userid", userid);
        //            await connection.OpenAsync();
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
    }
}
