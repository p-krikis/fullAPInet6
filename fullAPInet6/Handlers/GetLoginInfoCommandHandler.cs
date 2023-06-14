using fullAPInet6.Commands;
using fullAPInet6.Services;
using MediatR;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class GetLoginInfoCommandHandler : IRequestHandler<GetLoginInfoCommand, string>
    {
        private readonly UserInfoHandlingService _userInfoHandlingService;
        public GetLoginInfoCommandHandler (UserInfoHandlingService userInfoHandlingService)
        {
            _userInfoHandlingService = userInfoHandlingService;
        }
        public async Task<string> Handle(GetLoginInfoCommand request, CancellationToken cancellationToken)
        {
            string jsonString = JsonConvert.SerializeObject(request.Content);
            string result = await _userInfoHandlingService.AuthUser(jsonString);
            return result;
        }
    }
}
