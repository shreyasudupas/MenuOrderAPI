using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.AccessLayer.IAccessLayer
{
    public interface IUserBL
    {
        tblUser AddOrGetUserDetails(UserProfile userProfile);
    }
}
