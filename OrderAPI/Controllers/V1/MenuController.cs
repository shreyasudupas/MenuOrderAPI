using BuisnessLayer;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrderAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [EnableCors("AllowMyOrigin")]
    [Authorize(Policy = "AllowUserAccess")]
    public class MenuController : ControllerBase
    {
        private readonly IOrderBL _orderBL;
        public MenuController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        /// <summary>
        /// Get the Menu List from the Vendor ID
        /// </summary>
        /// <param name="VendorId"></param>
        /// <returns>Menu list</returns>
        [HttpGet]
        public async Task<IActionResult> GetMenuListAsync(int VendorId)
        {
            var headers = HttpContext.Request.Headers["UserInfo"];

            APIResponse response = new APIResponse();
            try
            {
                var result = await _orderBL.GetMenuListForVednorIdAsync(VendorId);
                if(result!=null)
                {
                    response.Content = result;
                    response.Response = 200;
                }
                else
                {
                    response.Response = 404;
                }
            }catch(Exception e)
            {
                response.Response = 500;
                response.Content = e.Message;
            }
            return Ok(response);
        }
    }
}
