using BuisnessLayer.AccessLayer.IAccessLayer;
using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using System;
using System.Linq;

namespace BuisnessLayer.AccessLayer
{
    public class UserBL:IUserBL
    {
        public MenuOrderManagementContext dbContext;

        public UserBL(MenuOrderManagementContext context)
        {
            dbContext = context;
        }

        public tblUser AddOrGetUserDetails(UserProfile userProfile)
        {
            tblUser user = new tblUser();
            try
            {
                var isUserPresent = dbContext.tblUsers.Where(x => x.UserName == userProfile.Username).FirstOrDefault();
                if(isUserPresent==null)
                {
                    user.UserName = userProfile.Username;
                    user.Nickname = userProfile.NickName;
                    user.PictureLocation = userProfile.PictureLocation;
                    user.RoleId = userProfile.RoleId;
                    user.Points = 0;
                    user.CartAmount = 0;
                    user.CreatedDate = DateTime.Now;

                    dbContext.tblUsers.Add(user);
                }
                else
                    isUserPresent = user;
                    
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {

            }
            return user;
        }
    }
}
