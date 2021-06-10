using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Controllers.V1.Models
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string PictureLocation { get; set; }
        public string NickName { get; set; }
    }
}
