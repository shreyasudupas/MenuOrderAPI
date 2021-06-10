using BuisnessLayer.Models;

namespace BuisnessLayer.AccessLayer.IModels
{
    public interface IProfileUser
    {
        public string EmailId { get; set; }

        string GetUserEmail();
    }
}
