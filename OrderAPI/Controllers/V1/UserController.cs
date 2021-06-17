using BuisnessLayer.AccessLayer.IAccessLayer;
using BuisnessLayer.AccessLayer.IModels;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [EnableCors("AllowMyOrigin")]
    [Authorize(Policy = "AllowUserAccess")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IProfileUser _profile;
        public UserController(IUserBL userBL,IProfileUser profile)
        {
            _userBL = userBL;
            _profile = profile;
        }

        /// <summary>
        /// Gets the user details and if not present then adds it
        /// </summary>
        /// <returns>User Profile Details</returns>
        /// <response code="200">success userDetails</response>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetOrUpdateUserDetails(UserProfile userProfile)
        {
            APIResponse response = new APIResponse();
            try
            {
                var res = _userBL.AddOrGetUserDetails(userProfile);
                if(res != null)
                {
                    response.Content = res;
                    response.Response = 200;
                }
                else
                {
                    response.Response = 404;
                }
            }catch(Exception ex)
            {
                response.Exception = ex.Message;
                response.Response = 500;
            }
            return Ok(response);
        }

    }
}
