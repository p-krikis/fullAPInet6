using fullAPInet6.Models;
using fullAPInet6.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly UserInfoHandlingService _userInfoHandling;
        private readonly ListDataHandlingService _listDataHandlingService;
        private readonly ListDataReconstructionService _listDataReconstructionService;

        public UserInfoController(UserInfoHandlingService userInfoHandling, ListDataHandlingService listDataHandlingService)
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
        public async Task<IActionResult> FetchUserInfo([FromBody] RequestData content)
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
        public async Task<IActionResult> DeleteUser([FromBody] RequestData content)
        {
            string userid = content.userId;
            await _userInfoHandling.DeleteUserByListId(userid);
            return Ok("Deleted");
        }
    }
}


//===ENDPOINTS===//
//https://localhost:7239/api/UserInfo/postSignupInfo        //
//https://localhost:7239/api/UserInfo/postLoginInfo         //
//https://localhost:7239/api/UserInfo/fetchUserInfo         //
//https://localhost:7239/api/UserInfo/updateInfo            //
//https://localhost:7239/api/UserInfo/deleteUser            //