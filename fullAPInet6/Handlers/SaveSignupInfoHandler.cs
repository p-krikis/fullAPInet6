using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class SaveSignupInfoHandler : IRequestHandler<SaveSignupInfoRequest, int>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public SaveSignupInfoHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<int> Handle(SaveSignupInfoRequest request, CancellationToken cancellationToken)
        {
            string jsonString = JsonConvert.SerializeObject(request.Content);
            return await _userInfoHandling.SaveUserInfo(jsonString);
        }
    }
}
