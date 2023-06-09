using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class FetchUserInfoRequest : IRequest<IActionResult>
    {
        public string UserId { get; }

        public FetchUserInfoRequest(string userId)
        {
            UserId = userId;
        }
    }
}
