using fullAPInet6.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Requests
{
    public class UpdateUserInfoRequest : IRequest<IActionResult>
    {
        public UpdatedInfo UpdatedInfo { get; }

        public UpdateUserInfoRequest(UpdatedInfo updatedInfo)
        {
            UpdatedInfo = updatedInfo;
        }
    }
}
