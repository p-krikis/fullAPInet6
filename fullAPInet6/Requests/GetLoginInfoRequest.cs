using fullAPInet6.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class GetLoginInfoRequest : IRequest<IActionResult>
    {
        public UserParsingModels Content { get; }

        public GetLoginInfoRequest(UserParsingModels content)
        {
            Content = content;
        }
    }
}
