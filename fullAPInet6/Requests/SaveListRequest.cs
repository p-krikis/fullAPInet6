using fullAPInet6.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests {
    public class SaveListRequest : IRequest<IActionResult>
    {
        public ListParsingModels Content { get; }

        public SaveListRequest(ListParsingModels content)
        {
            Content = content;
        }
    }
}
