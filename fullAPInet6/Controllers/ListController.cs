using fullAPInet6.Models;
using fullAPInet6.Services;
using Microsoft.AspNetCore.Mvc;

namespace fullAPInet6.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListService _listService;

        public ListController(ListService listService)
        {
            _listService = listService;
        }

        [HttpPost("saveList")]
        public async Task<IActionResult> SaveList([FromBody] ListModels list)
        {
            var result = await _listService.ListSave(list);
            if (result != null)
            {
                return StatusCode(201, $"Saved list with the name{list.ListName}");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("loadAllLists/{userID}")]
        public async Task<IActionResult> LoadAllLists(string userID)
        {
            var result = await _listService.GetAllLists(userID);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("loadSingleList/{userID}/{listName}")]
        public async Task<IActionResult> LoadSingleList (string userID, string listName)
        {
            var result = await _listService.LoadList(userID, listName);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("updateList/{listID}")]
        public async Task<IActionResult> UpdateList(string listID, [FromBody]ListModels listTBU)
        {
            await _listService.UpdateList(listID, listTBU);
            return Ok("Updated");
        }
        [HttpDelete("deleteList/{listID}")]
        public async Task<IActionResult> DeleteList(string listID)
        {
            await _listService.DeleteList(listID);
            return Ok("Deleted");
        }
    }
}


//===ENDPOINTS===//
//https://localhost:7239/api/v1/List/saveList                                   //
//https://localhost:7239/api/v1/List/loadAllLists/{userID}                      //
//https://localhost:7239/api/v1/List/loadSingleList/{userID}/{listName}         //
//https://localhost:7239/api/v1/List/updateList/{listID}                        //
//https://localhost:7239/api/v1/List/deleteList/{listID}                        //
