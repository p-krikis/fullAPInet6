using fullAPInet6.Models;
using fullAPInet6.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> UserSignup ([FromBody] UserModels userInfo)
        {
            var checkResult = await _userService.AddUser(userInfo);
            if (checkResult ==  null)
            {
                return StatusCode(201, "Signed up successfully");
            }
            else
            {
                return BadRequest("Email is already in use");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin ([FromBody] LoginModels loginInfo)
        {
            var authResult = await _userService.LoginUser(loginInfo);
            if (authResult == null)
            {
                return StatusCode(401, "Submitted credentials are incorrect");
            }
            else
            {
                return Ok(authResult);
            }
        }
        [HttpGet("fetch/{userID}")]
        public async Task<IActionResult> UserFetch (string userID)
        {
            var response = await _userService.FetchUser(userID);
            return Ok(response);
        }
        [HttpPut("update/{userID}")]
        public async Task<IActionResult> UserUpdate (string userID, [FromBody]UserUpdateModels updatedInfo)
        {
            await _userService.UpdateUser(userID, updatedInfo);
            return Ok();
        }
        [HttpDelete("delete/{userID}")]
        public async Task<IActionResult> UserDelete (string userID)
        {
            await _userService.DeleteUser(userID);
            return Ok();
        }
    }
}


//===ENDPOINTS===//
//https://localhost:7239/api/v1/User/signup                     //
//https://localhost:7239/api/v1/User/login                      //
//https://localhost:7239/api/v1/User/fetch/{userID}             //
//https://localhost:7239/api/v1/User/update/{userID}            //
//https://localhost:7239/api/v1/User/delete/{userID}            //

//private readonly UserInfoHandlingService _userInfoHandling;
//private readonly ListDataHandlingService _listDataHandlingService;
//private readonly ListDataReconstructionService _listDataReconstructionService;

//public UserController(UserInfoHandlingService userInfoHandling, ListDataHandlingService listDataHandlingService)
//{
//    _userInfoHandling = userInfoHandling;
//    _listDataHandlingService = listDataHandlingService;
//    _listDataReconstructionService = new ListDataReconstructionService();
//}

//[HttpPost("postSignupInfo")]
//public async Task<IActionResult> SaveSignupInfo([FromBody] UserParsingModels content)
//{
//    string jsonString = JsonConvert.SerializeObject(content);
//    int userId = await _userInfoHandling.SaveUserInfo(jsonString);
//    return Ok();
//}

//[HttpPost("postLoginInfo")]
//public async Task<IActionResult> GetLoginInfo([FromBody] UserParsingModels content)
//{
//    string jsonString = JsonConvert.SerializeObject(content);
//    string result = await _userInfoHandling.AuthUser(jsonString);
//    if (result == null)
//    {
//        return Unauthorized();
//    }
//    else
//    {
//        return Ok(result);
//    }
//}
//[HttpPost("fetchUserInfo")]
//public async Task<IActionResult> FetchUserInfo([FromBody] RequestData content)
//{
//    string userid = content.userId;
//    var userInfo = await _userInfoHandling.UserInfo(userid);
//    return Ok(userInfo);
//}
//[HttpPut("updateInfo")]
//public async Task<IActionResult> UpdateUserInfo([FromBody] UpdatedInfo updInfo)
//{
//    string targetUID = updInfo.UserId;
//    string jsonString = JsonConvert.SerializeObject(updInfo);
//    await _userInfoHandling.UpdateUserInfo(targetUID, jsonString);
//    return Ok();
//}

//[HttpDelete("deleteUser")]
//public async Task<IActionResult> DeleteUser([FromBody] RequestData content)
//{
//    string userid = content.userId;
//    await _userInfoHandling.DeleteUserByListId(userid);
//    return Ok("Deleted");
//}