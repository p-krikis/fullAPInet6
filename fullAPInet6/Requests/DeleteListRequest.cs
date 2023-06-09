using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class DeleteListRequest : IRequest<IActionResult>
    {
        public string UserId { get; }
        public string ListName { get; }

        public DeleteListRequest(string userId, string listName)
        {
            UserId = userId;
            ListName = listName;
        }
    }
}
