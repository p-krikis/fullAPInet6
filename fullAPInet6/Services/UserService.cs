using fullAPInet6.Models;
using MongoDB.Driver;

namespace fullAPInet6.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModels> _users;
        private readonly IMongoCollection<ListModels> _lists;

        public UserService()
        {
            MongoClient client = new("mongodb+srv://<USERNAME_HERE>:<PASSWORD_HERE>@fullstackclustertest.qdnrxbu.mongodb.net/?retryWrites=true&w=majority");
            IMongoDatabase db = client.GetDatabase("FullstackDB");
            _users = db.GetCollection<UserModels>("Users");
            _lists = db.GetCollection<ListModels>("Lists");
        }
        public async Task<string> AddUser(UserModels user)
        {
            var userCheck = await _users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (userCheck != null)
            {
                return "CF";
            }
            else
            {
                await _users.InsertOneAsync(user);
                return null;
            }
        }
        public async Task<string> LoginUser(LoginModels user)
        {
            var authCheck = new UserModels();
            if (user.Email == null)
            {
                authCheck = await _users.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
            }
            else
            {
                authCheck = await _users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
            }
            
            if (authCheck != null)
            {
                if (authCheck.Password == user.Password)
                {
                    return authCheck.UserID;
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
        public async Task<UserModels> FetchUser (string userID)
        {
            var result = await _users.Find(u => u.UserID == userID).FirstOrDefaultAsync();
            return result;
        }
        public async Task UpdateUser (string userID, UserUpdateModels userInfo)
        {
            var fetchExistingInfo = await _users.Find(u => u.UserID == userID).FirstOrDefaultAsync();
            UserModels userTBU = new UserModels();
            
            if (userInfo.Username == null)
            {
                userTBU.Username = fetchExistingInfo.Username;
            }
            else
            {
                userTBU.Username=userInfo.Username;
            }
            if (userInfo.Password == null)
            {
                userTBU.Password = fetchExistingInfo.Password;
            }
            else
            {
                userTBU.Password=userInfo.Password;
            }
            if (userInfo.Email == null)
            {
                userTBU.Email = fetchExistingInfo.Email;
            }
            else
            {
                userTBU.Email=userInfo.Email;
            }
            if (userInfo.Role == null)
            {
                userTBU.Role = fetchExistingInfo.Role;
            }
            else
            {
                userTBU.Role=userInfo.Role;
            }
            userTBU.UserID = userID;
            userTBU.AccountName = fetchExistingInfo.AccountName;
            await _users.ReplaceOneAsync(u => u.UserID == userID, userTBU);
        }
        public async Task DeleteUser (string userID)
        {
            await _users.DeleteOneAsync(u => u.UserID == userID);
        }
    }
}