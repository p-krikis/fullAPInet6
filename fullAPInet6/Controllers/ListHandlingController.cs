using fullAPInet6.Models;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListHandlingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListHandlingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[HttpPost("saveListData")]
        //public async Task<IActionResult> SaveList([FromBody] ListParsingModels content)
        //{
        //    string listName = content.ListName;
        //    string userId = content.UserId;
        //    string jsonData = JsonConvert.SerializeObject(content);
        //    int listId = await _listDataHandlingService.SaveListData(listName, userId, jsonData);
        //    return Ok();
        //}

        //[HttpPost("getAllLists")]
        //public async Task<IActionResult> LoadAllLists([FromBody] RequestData content)
        //{
        //    string targetUserId = content.userId;
        //    var lists = await _listDataHandlingService.LoadAllLists(targetUserId);
        //    return Ok(lists);
        //}

        //[HttpPost("loadspecificlist")]
        //public async Task<IActionResult> loadspecificlist([FromBody] RequestData content)
        //{
        //    string targetuserid = content.userId;
        //    string targetlistname = content.listName;
        //    var listcontents = await _listDataHandlingService.LoadSingleList(targetuserid, targetlistname);
        //    ListParsingModels listData = JsonConvert.DeserializeObject<ListParsingModels>(listcontents);
        //    var listInfo = _listDataReconstructionService.RebuildData(listData);
        //    return Ok(listInfo);
        //}

        //[HttpDelete("deleteList")]
        //public async Task<IActionResult> DeleteList([FromBody] RequestData content)
        //{
        //    string targetUserId = content.userId;
        //    string targetListName = content.listName;
        //    await _listDataHandlingService.DeleteSpecificList(targetUserId, targetListName);
        //    return Ok("deleted");
        //}
    }
}


//===ENDPOINTS===//
//https://localhost:7239/api/ListHandling/saveListData      //
//https://localhost:7239/api/ListHandling/loadAllLists      //
//https://localhost:7239/api/ListHandling/loadSpecificList  //
//https://localhost:7239/api/ListHandling/deleteList        //