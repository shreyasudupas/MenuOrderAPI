using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Models
{
    public class APIResponse
    {
        public int Response { get; set; }
        public Object Content { get; set; }
        public Object Exception { get; set; }
    }
}
