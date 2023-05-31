namespace fullAPInet6.Models
{
    public class ListParsingModels
    {
        public string[]? NameData { get; set; }
        public string[]? DescData { get; set; }
        public string? UserId { get; set; }
        public string? ListName { get; set; }
    }

    public class ReturnType
    {
        public string listName { get; set; }
        public string timeCreated { get; set; }
    }
}