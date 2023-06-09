using MediatR;

using Microsoft.AspNetCore.Mvc;
namespace fullAPInet6.Requests
{
    public class DeleteUserRequest : IRequest<IActionResult>
    {
        public string UserId { get; }

        public DeleteUserRequest(string userId)
        {
            UserId = userId;
        }
    }
}
