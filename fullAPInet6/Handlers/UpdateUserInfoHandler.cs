using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoRequest, IActionResult>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public UpdateUserInfoHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<IActionResult> Handle(UpdateUserInfoRequest request, CancellationToken cancellationToken)
        {
            string targetUID = request.UpdatedInfo.UserId;
            string jsonString = JsonConvert.SerializeObject(request.UpdatedInfo);
            await _userInfoHandling.UpdateUserInfo(targetUID, jsonString);
            return new OkResult();
        }
    }
}
