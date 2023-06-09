using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class GetLoginInfoHandler : IRequestHandler<GetLoginInfoRequest, IActionResult>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public GetLoginInfoHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<IActionResult> Handle(GetLoginInfoRequest request, CancellationToken cancellationToken)
        {
            string jsonString = JsonConvert.SerializeObject(request.Content);
            string result = await _userInfoHandling.AuthUser(jsonString);
            if (result == null)
            {
                return new UnauthorizedResult();
            }
            else
            {
                return new OkObjectResult(result);
            }
        }
    }
}
