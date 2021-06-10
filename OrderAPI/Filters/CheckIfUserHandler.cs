using BuisnessLayer.AccessLayer;
using BuisnessLayer.AccessLayer.IModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Filters
{
    /// <summary>
    /// Check if the Role is user then allow access to those
    /// </summary>
    public class CheckIfUserHandler : AuthorizationHandler<UserRequirement>
    {
        private readonly IProfileUser _profile;

        public CheckIfUserHandler(IProfileUser profile)
        {
            _profile = profile;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement)
        {
            if(context.Resource is Endpoint endpoint)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var PermissionList = context.User.Claims.FirstOrDefault(c => c.Type == "permissions").Value;
                    var permisionSplit = PermissionList.Split(':');
                    var UserPermisson = permisionSplit[1];
                    if (UserPermisson.ToLower() == requirement.User.ToLower())
                    {
                        //Userprofile value is got from Middleware where the header value is stored
                        if(!string.IsNullOrEmpty(_profile.EmailId))
                        {
                            var getEmailId = _profile.GetUserEmail();
                            //succeded the request
                            context.Succeed(requirement);
                        }
                    }
                    
                }
                else
                {
                    return Task.CompletedTask;
                }
            }
           

            return Task.CompletedTask;
        }
    }
}
