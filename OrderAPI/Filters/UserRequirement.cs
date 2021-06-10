using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Filters
{
    public class UserRequirement : IAuthorizationRequirement
    {
        public string User { get; set; }
        public UserRequirement(string User)
        {
            this.User = User;
        }

    }
}
