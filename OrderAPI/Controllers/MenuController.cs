﻿using BuisnessLayer;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
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
        public IActionResult GetMenuList(int VendorId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var result = _orderBL.GetMenuListForVednorId(VendorId);
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