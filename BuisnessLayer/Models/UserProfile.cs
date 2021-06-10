using BuisnessLayer.AccessLayer.IModels;

namespace BuisnessLayer.Models
{
    public class UserProfile
    {
            public string Username { get; set; }
            public string PictureLocation { get; set; }
            public string NickName { get; set; }
            public int RoleId { get; set; }
    }
}
