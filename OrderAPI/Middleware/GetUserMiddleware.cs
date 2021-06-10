﻿using BuisnessLayer.AccessLayer;
using BuisnessLayer.AccessLayer.IModels;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Middleware
{
    public class GetUserMiddleware
    {
        private readonly RequestDelegate _next;
        public GetUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IProfileUser profile)
        {
            try
            {
                //get UserInfo from header
                var User = JsonConvert.DeserializeObject<UserProfile>(context.Request.Headers["UserInfo"]);

                if(!string.IsNullOrEmpty(User.Username))
                    profile.EmailId = User.Username;
            }
            catch (Exception)
            {
            }

            await _next(context);
        }
    }
}