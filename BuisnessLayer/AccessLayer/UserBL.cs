using AutoMapper;
using BuisnessLayer.AccessLayer.IAccessLayer;
using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using OrderAPI.BuisnessLayer.Models;
using System;
using System.Linq;

namespace BuisnessLayer.AccessLayer
{
    public class UserBL:IUserBL
    {
        public MenuOrderManagementContext dbContext;
        private readonly IMapper _mapper;

        public UserBL(MenuOrderManagementContext context, IMapper mapper)
        {
            dbContext = context;
            _mapper = mapper;
        }

        public User AddOrGetUserDetails(UserProfile userProfile)
        {
            User UserProfile = new User();
            try
            {
                var user = dbContext.tblUsers.Where(x => x.UserName == userProfile.Username).FirstOrDefault();
                if(user == null)
                {
                    user.UserName = userProfile.Username;
                    user.FullName = userProfile.NickName;
                    user.PictureLocation = userProfile.PictureLocation;
                    user.RoleId = userProfile.RoleId;
                    user.Points = 0;
                    user.CartAmount = 0;
                    user.CreatedDate = DateTime.Now;

                    dbContext.tblUsers.Add(user);
                    dbContext.SaveChanges();
                }
                UserProfile = _mapper.Map<User>(user);
            }
            catch(Exception ex)
            {

            }
            return UserProfile;
        }
    }
}
