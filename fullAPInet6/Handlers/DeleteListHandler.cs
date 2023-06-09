using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Handlers
{
    public class DeleteListHandler : IRequestHandler<DeleteListRequest, IActionResult>
    {
        private readonly ListDataHandlingService _listDataHandlingService;

        public DeleteListHandler(ListDataHandlingService listDataHandlingService)
        {
            _listDataHandlingService = listDataHandlingService;
        }

        public async Task<IActionResult> Handle(DeleteListRequest request, CancellationToken cancellationToken)
        {
            await _listDataHandlingService.DeleteSpecificList(request.UserId, request.ListName);
            return new OkObjectResult("Deleted");
        }
    }
}
