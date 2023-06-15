using fullAPInet6.Models;
using MediatR;

namespace fullAPInet6.Commands
{
    public record FetchProfileInfoCommand(RequestData Content) : IRequest<List<string>>;
}
