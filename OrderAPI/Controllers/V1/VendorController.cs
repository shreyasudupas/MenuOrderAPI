using BuisnessLayer;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrderAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [EnableCors("AllowMyOrigin")]
    [Authorize]
    public class VendorController : ControllerBase
    {
        private readonly IOrderBL _orderBL;
        public VendorController(IOrderBL orderBL)
        {
            _orderBL = orderBL;
        }

        /// <summary>
        /// This is to get the Vendor List
        /// </summary>
        /// <returns>List of string</returns>
        /// <response code="404">No Vendors</response>
        /// <response code="500">Exception in code</response>
        [HttpGet]
        public async Task<IActionResult> GetVendorListAsync()
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                var getResult = await _orderBL.GetVendorListAsync();
                if (getResult.Count > 0)
                {
                    aPIResponse.Content = getResult;
                    aPIResponse.Response = 200;
                }
                else
                {
                    aPIResponse.Response = 404;
                }
            }catch(Exception ex)
            {
                aPIResponse.Response = 500;
                aPIResponse.Exception = ex.Message;
            }
            return Ok(aPIResponse);
        }
    }
}
