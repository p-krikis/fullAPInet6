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
        private readonly ListDataHandlingService _listHandlingService;

        public RequestHandlingController(UserInfoHandlingService userInfoHandling, ListDataHandlingService listHandlingService)
        {
            _userInfoHandling = userInfoHandling;
            _listHandlingService = listHandlingService;
        }

        [HttpPost("postSignupInfo")]
        public async Task<IActionResult> SaveSignupInfo([FromBody] ParsingModels content)
        {
            string jsonString = JsonConvert.SerializeObject(content);
            int userId = await _userInfoHandling.SaveUserInfo(jsonString);
            return Ok("check db for results");
        }

        [HttpPost("postLoginInfo")]
        public async Task<IActionResult> GetLoginInfo([FromBody] ParsingModels content)
        {
            string jsonString = JsonConvert.SerializeObject(content);
            string result = await _userInfoHandling.AuthUser(jsonString);
            if (result == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete("deleteUserByListId/{userListId}")]
        public async Task<IActionResult> DeleteUser(int userListId)
        {
            await _userInfoHandling.DeleteUserByListId(userListId);
            return Ok("Deleted");
        }

    }
}
