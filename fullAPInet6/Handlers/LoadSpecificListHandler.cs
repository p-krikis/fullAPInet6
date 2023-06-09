using fullAPInet6.Models;
using fullAPInet6.Requests;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Handlers
{
    public class LoadSpecificListHandler : IRequestHandler<LoadSpecificListRequest, IActionResult>
    {
        private readonly ListDataHandlingService _listDataHandlingService;
        private readonly ListDataReconstructionService _listDataReconstructionService;

        public LoadSpecificListHandler(ListDataHandlingService listDataHandlingService, ListDataReconstructionService listDataReconstructionService)
        {
            _listDataHandlingService = listDataHandlingService;
            _listDataReconstructionService = listDataReconstructionService;
        }

        public async Task<IActionResult> Handle(LoadSpecificListRequest request, CancellationToken cancellationToken)
        {
            var listcontents = await _listDataHandlingService.LoadSingleList(request.UserId, request.ListName);
            ListParsingModels listData = JsonConvert.DeserializeObject<ListParsingModels>(listcontents);
            var listInfo = _listDataReconstructionService.RebuildData(listData);
            return new OkObjectResult(listInfo);
        }
    }
}
