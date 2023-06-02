using System.Diagnostics.Contracts;

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
        public int? listid { get; set; }
        public string? listName { get; set; }
        public string? timeCreated { get; set; }
    }

    public class ReturnData
    {
        public string[]? nameData { get; set; }
        public string[]? descData { get; set; }
    }
}