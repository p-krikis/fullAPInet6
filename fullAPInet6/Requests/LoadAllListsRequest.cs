using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class LoadAllListsRequest : IRequest<IActionResult>
    {
        public string UserId { get; }

        public LoadAllListsRequest(string userId)
        {
            UserId = userId;
        }
    }
}
