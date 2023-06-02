namespace fullAPInet6.Models
{
    public class UserParsingModels
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserId { get; set; }
    }

    public class Requests
    {
        public string? userId { get; set; }
        public string? listName { get; set; }
    }
}