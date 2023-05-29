using fullAPInet6.Models;
using fullAPInet6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestHandlingController : ControllerBase
    {
        private readonly UserInfoHandlingService _userInfoHandling;
        private readonly ListDataHandlingService _listDataHandlingService;

        public RequestHandlingController(UserInfoHandlingService userInfoHandling, ListDataHandlingService listDataHandlingService)
        {
            _userInfoHandling = userInfoHandling;
            _listDataHandlingService = listDataHandlingService;
        }

        [HttpPost("postSignupInfo")]
        public async Task<IActionResult> SaveSignupInfo([FromBody] UserParsingModels content)
        {
            string jsonString = JsonConvert.SerializeObject(content);
            int userId = await _userInfoHandling.SaveUserInfo(jsonString);
            return Ok();
        }

        [HttpPost("postLoginInfo")]
        public async Task<IActionResult> GetLoginInfo([FromBody] UserParsingModels content)
        {
            string jsonString = JsonConvert.SerializeObject(content);
            string result = await _userInfoHandling.AuthUser(jsonString);
            if (result == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete("deleteUserByListId/{userListId}")]
        public async Task<IActionResult> DeleteUser(int userListId)
        {
            await _userInfoHandling.DeleteUserByListId(userListId);
            return Ok("Deleted");
        }

        //listHandling below

        [HttpPost("saveListData")]
        public async Task<IActionResult>SaveList([FromBody] ListParsingModels content)
        {
            string jsonData = JsonConvert.SerializeObject(content);
            int listId = await _listDataHandlingService.SaveListData(jsonData);
            return Ok($"Saved with id: {listId}");
        }
        [HttpGet("getAllLists")]
        public async Task<IActionResult> LoadAllLists([FromBody] Requests content)
        {
            string targetUserId = content.userId;
            var lists = await _listDataHandlingService.LoadAllLists(targetUserId);
            return Ok(lists);
        }
        [HttpPost("loadSpecificList")]
        public async Task<IActionResult> LoadSpecificList([FromBody] Requests content)
        {
            string targetUserId = content.userId;
            string targetListName = content.listName;
            dynamic listContents = _listDataHandlingService.LoadSingleList(targetUserId, targetListName);
            return Ok(listContents);
        }
        [HttpDelete("deleteList")]
        public async Task<IActionResult> DeleteList([FromBody] Requests content)
        {
            string targetUserId = content.userId;
            string targetListName = content.listName;
            await _listDataHandlingService.DeleteSpecificList(targetUserId, targetListName);
            return Ok();
        }
    }
}
