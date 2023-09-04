using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fullAPInet6.Models
{
    public class UserModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserID { get; set; }
        public string AccountName { get; set; }
        public string? Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
    public class LoginModels
    {
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Username { get; set; }
    }
    public class UserUpdateModels
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
    public class ListModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ListID { get; set; }
        public string? ListName { get; set; }
        public string UserID { get; set; }
        public string[]? MainData { get; set; }
        public string[]? DescData { get; set; }
    }

    //public class UserParsingModels
    //{
    //    public string? Username { get; set; }
    //    public string? Email { get; set; }
    //    public string? Password { get; set; }
    //    public string? UserId { get; set; }
    //    public string? DisplayName { get; set; }
    //    public string? Role { get; set; }
    //}

    //public class RequestData
    //{
    //    public string? userId { get; set; }
    //    public string? listName { get; set; }
    //}
    //public class UserInfoResponse
    //{
    //    public string? email { get; set; }
    //    public string? username { get; set; }
    //    public string? role { get; set; }
    //    public string? displayName { get; set; }
    //}
    //public class UpdatedInfo
    //{
    //    public string? UserId { get; set; }
    //    public string? Email { get; set;}
    //    public string? Username { get; set;}
    //    public string? DisplayName { get; set; }
    //    public string? Role { get; set;}
    //}
}