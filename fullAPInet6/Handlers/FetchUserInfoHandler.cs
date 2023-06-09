using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Handlers
{
    public class FetchUserInfoHandler : IRequestHandler<FetchUserInfoRequest, IActionResult>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public FetchUserInfoHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<IActionResult> Handle(FetchUserInfoRequest request, CancellationToken cancellationToken)
        {
            var userInfo = await _userInfoHandling.UserInfo(request.UserId);
            return new OkObjectResult(userInfo);
        }
    }
}
