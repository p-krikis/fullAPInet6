using fullAPInet6.Models;
using MediatR;

namespace fullAPInet6.Commands
{
    public record GetLoginInfoCommand(UserParsingModels Content) : IRequest<string>;
}
