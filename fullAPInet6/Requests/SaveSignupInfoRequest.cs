using fullAPInet6.Models;
using MediatR;

namespace fullAPInet6.Requests
{
    public class SaveSignupInfoRequest :IRequest<int>
    {
        public UserParsingModels Content { get; }

        public SaveSignupInfoRequest(UserParsingModels content)
        {
            Content = content;
        }
    }
}
