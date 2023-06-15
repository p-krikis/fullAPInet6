using fullAPInet6.Commands;
using fullAPInet6.Models;
using fullAPInet6.Services;
using MediatR;

namespace fullAPInet6.Handlers
{
    public class FetchProfileInfoCommandHandler : IRequestHandler<FetchProfileInfoCommand, List<string>>
    {
        private readonly UserInfoHandlingService _userInfoHandlingService;

        public FetchProfileInfoCommandHandler(UserInfoHandlingService userInfoHandlingService)
        {
            _userInfoHandlingService = userInfoHandlingService;
        }

        public async Task<List<string>> Handle(FetchProfileInfoCommand request, CancellationToken cancellationToken)
        {
            string userid = request.Content.userId;
            List<UserInfoResponse> userInfo = await _userInfoHandlingService.UserInfo(userid);
            return userInfo;
        }
    }
}
