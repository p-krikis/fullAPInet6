using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class LoadSpecificListRequest : IRequest<IActionResult>
    {
        public string UserId { get; }
        public string ListName { get; }

        public LoadSpecificListRequest(string userId, string listName)
        {
            UserId = userId;
            ListName = listName;
        }
    }
}
