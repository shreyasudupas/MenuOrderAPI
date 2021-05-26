using BuisnessLayer;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
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
        public IActionResult GetVendorList()
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                var getResult = _orderBL.GetVendorList();
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
