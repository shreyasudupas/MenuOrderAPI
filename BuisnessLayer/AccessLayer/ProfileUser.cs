using BuisnessLayer.AccessLayer.IModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.AccessLayer
{
    /// <summary>
    /// This is used to send the Email Id across the diffrent layers in Scopped Lifetime from Middleware to Authorization to Controller the value Email Id can be read
    /// </summary>
    /// <retruns>The EmailID</retruns>
    public class ProfileUser : IProfileUser
    {
        public string EmailId { get; set; }

        public string GetUserEmail()
        {
            return EmailId; 
        }
    }
}
