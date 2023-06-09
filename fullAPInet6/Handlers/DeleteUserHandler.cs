using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, IActionResult>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public DeleteUserHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<IActionResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await _userInfoHandling.DeleteUserByListId(request.UserId);
            return new OkObjectResult("Deleted");
        }
    }
}
