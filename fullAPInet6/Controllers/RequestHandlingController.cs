using fullAPInet6.Models;
using fullAPInet6.Services;
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
        private readonly ListDataReconstructionService _listDataReconstructionService;

        public RequestHandlingController(UserInfoHandlingService userInfoHandling, ListDataHandlingService listDataHandlingService)
        {
            _userInfoHandling = userInfoHandling;
            _listDataHandlingService = listDataHandlingService;
            _listDataReconstructionService = new ListDataReconstructionService();
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
        [HttpPost("fetchUserInfo")]
        public async Task<IActionResult> FetchUserInfo([FromBody] Requests content)
        {
            string userid = content.userId;
            var userInfo = await _userInfoHandling.UserInfo(userid);
            return Ok(userInfo);
        }
        [HttpPut("updateInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdatedInfo updInfo) 
        {
            string targetUID = updInfo.UserId;
            string jsonString = JsonConvert.SerializeObject(updInfo);
            await _userInfoHandling.UpdateUserInfo(targetUID, jsonString);
            return Ok();
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] Requests content)
        {
            string userid = content.userId;
            await _userInfoHandling.DeleteUserByListId(userid);
            return Ok("Deleted");
        }

        //-----------------------listHandling below------------------------//

        [HttpPost("saveListData")]
        public async Task<IActionResult> SaveList([FromBody] ListParsingModels content)
        {
            string listName = content.ListName;
            string userId = content.UserId;
            string jsonData = JsonConvert.SerializeObject(content);
            int listId = await _listDataHandlingService.SaveListData(listName, userId, jsonData);
            return Ok();
        }

        [HttpPost("getAllLists")]
        public async Task<IActionResult> LoadAllLists([FromBody] Requests content)
        {
            string targetUserId = content.userId;
            var lists = await _listDataHandlingService.LoadAllLists(targetUserId);
            return Ok(lists);
        }

        [HttpPost("loadspecificlist")]
        public async Task<IActionResult> loadspecificlist([FromBody] Requests content)
        {
            string targetuserid = content.userId;
            string targetlistname = content.listName;
            var listcontents = await _listDataHandlingService.LoadSingleList(targetuserid, targetlistname);
            ListParsingModels listData = JsonConvert.DeserializeObject<ListParsingModels>(listcontents);
            var listInfo = _listDataReconstructionService.RebuildData(listData);
            return Ok(listInfo);
        }

        [HttpDelete("deleteList")]
        public async Task<IActionResult> DeleteList([FromBody] Requests content)
        {
            string targetUserId = content.userId;
            string targetListName = content.listName;
            await _listDataHandlingService.DeleteSpecificList(targetUserId, targetListName);
            return Ok("deleted");
        }
    }
}

//https://localhost:7239/api/RequestHandling/getAllLists