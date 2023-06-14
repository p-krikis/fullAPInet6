using fullAPInet6.Commands;
using fullAPInet6.Models;
using fullAPInet6.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fullAPInet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("postSignupInfo")]
        public async Task<IActionResult> SaveSignupInfo([FromBody] UserParsingModels content)
        {
            var command = new SaveSignupInfoCommand(content);
            int userId = await _mediator.Send(command);
            return StatusCode(201);
        }
        [HttpPost("submittedInfo")]
        public async Task<IActionResult> CheckLoginInfo([FromBody] UserParsingModels content)
        {
            var command = new GetLoginInfoCommand(content);
            string returnedResult = await _mediator.Send(command);
            if (returnedResult != null)
            {
                return Ok(returnedResult);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

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
}


//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace fullAPInet6.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RequestHandlingController : ControllerBase
//    {
//        private readonly IMediator _mediator;

//        public RequestHandlingController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpPost("postSignupInfo")]
//        public async Task<IActionResult> SaveSignupInfo([FromBody] UserParsingModels content)
//        {
//            var request = new SaveSignupInfoRequest(content);
//            int userId = await _mediator.Send(request);
//            return Ok();
//        }

//        [HttpPost("postLoginInfo")]
//        public async Task<IActionResult> GetLoginInfo([FromBody] UserParsingModels content)
//        {
//            var request = new GetLoginInfoRequest(content);
//            IActionResult result = await _mediator.Send(request);
//            return result;
//        }

//        [HttpPost("fetchUserInfo")]
//        public async Task<IActionResult> FetchUserInfo([FromBody] RequestData content)
//        {
//            var request = new FetchUserInfoRequest(content.userId);
//            IActionResult userInfo = await _mediator.Send(request);
//            return userInfo;
//        }

//        [HttpPut("updateInfo")]
//        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdatedInfo updInfo)
//        {
//            var request = new UpdateUserInfoRequest(updInfo);
//            IActionResult result = await _mediator.Send(request);
//            return result;
//        }

//        [HttpDelete("deleteUser")]
//        public async Task<IActionResult> DeleteUser([FromBody] RequestData content)
//        {
//            var request = new DeleteUserRequest(content.userId);
//            IActionResult result = await _mediator.Send(request);
//            return result;
//        }

//        [HttpPost("saveListData")]
//        public async Task<IActionResult> SaveList([FromBody] ListParsingModels content)
//        {
//            var request = new SaveListRequest(content);
//            IActionResult result = await _mediator.Send(request);
//            return result;
//        }

//        [HttpPost("getAllLists")]
//        public async Task<IActionResult> LoadAllLists([FromBody] RequestData content)
//        {
//            var request = new LoadAllListsRequest(content.userId);
//            IActionResult lists = await _mediator.Send(request);
//            return lists;
//        }

//        [HttpPost("loadspecificlist")]
//        public async Task<IActionResult> loadspecificlist([FromBody] RequestData content)
//        {
//            var request = new LoadSpecificListRequest(content.userId, content.listName);
//            IActionResult listInfo = await _mediator.Send(request);
//            return listInfo;
//        }

//        [HttpDelete("deleteList")]
//        public async Task<IActionResult> DeleteList([FromBody] RequestData content)
//        {
//            var request = new DeleteListRequest(content.userId, content.listName);
//            IActionResult result = await _mediator.Send(request);
//            return result;
//        }
//    }
//}


//===ENDPOINTS===//
//https://localhost:7239/api/UserInfo/postSignupInfo        //
//https://localhost:7239/api/UserInfo/postLoginInfo         //
//https://localhost:7239/api/UserInfo/fetchUserInfo         //
//https://localhost:7239/api/UserInfo/updateInfo            //
//https://localhost:7239/api/UserInfo/deleteUser            //