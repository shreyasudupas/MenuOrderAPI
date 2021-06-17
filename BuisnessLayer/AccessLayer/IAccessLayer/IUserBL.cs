using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using OrderAPI.BuisnessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.AccessLayer.IAccessLayer
{
    public interface IUserBL
    {
        User AddOrGetUserDetails(UserProfile userProfile);
    }
}
