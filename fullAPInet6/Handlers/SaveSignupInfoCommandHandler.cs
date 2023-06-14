using fullAPInet6.Commands;
using fullAPInet6.Services;
using MediatR;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class SaveSignupInfoCommandHandler : IRequestHandler<SaveSignupInfoCommand, int>
    {
        private readonly UserInfoHandlingService _userInfoHandling;

        public SaveSignupInfoCommandHandler(UserInfoHandlingService userInfoHandling)
        {
            _userInfoHandling = userInfoHandling;
        }

        public async Task<int> Handle(SaveSignupInfoCommand request, CancellationToken cancellationToken)
        {
            string jsonString = JsonConvert.SerializeObject(request.Content);
            int userId = await _userInfoHandling.SaveUserInfo(jsonString);
            return userId;
        }
    }
}
