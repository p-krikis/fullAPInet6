using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Handlers
{
    public class LoadAllListsHandler : IRequestHandler<LoadAllListsRequest, IActionResult>
    {
        private readonly ListDataHandlingService _listDataHandlingService;

        public LoadAllListsHandler(ListDataHandlingService listDataHandlingService)
        {
            _listDataHandlingService = listDataHandlingService;
        }

        public async Task<IActionResult> Handle(LoadAllListsRequest request, CancellationToken cancellationToken)
        {
            var lists = await _listDataHandlingService.LoadAllLists(request.UserId);
            return new OkObjectResult(lists);
        }
    }
}
