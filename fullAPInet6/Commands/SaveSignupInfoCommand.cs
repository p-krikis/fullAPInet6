using fullAPInet6.Models;
using MediatR;

namespace fullAPInet6.Commands
{
    public record SaveSignupInfoCommand(UserParsingModels Content) : IRequest<int>;
}
