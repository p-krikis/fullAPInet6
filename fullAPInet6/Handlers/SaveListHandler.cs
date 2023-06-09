using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class SaveListHandler : IRequestHandler<SaveListRequest, IActionResult>
    {
        private readonly ListDataHandlingService _listDataHandlingService;

        public SaveListHandler(ListDataHandlingService listDataHandlingService)
        {
            _listDataHandlingService = listDataHandlingService;
        }

        public async Task<IActionResult> Handle(SaveListRequest request, CancellationToken cancellationToken)
        {
            string listName = request.Content.ListName;
            string userId = request.Content.UserId;
            string jsonData = JsonConvert.SerializeObject(request.Content);
            await _listDataHandlingService.SaveListData(listName, userId, jsonData);
            return new OkResult();
        }
    }
}
