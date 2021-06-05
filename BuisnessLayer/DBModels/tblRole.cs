using System.Collections.Generic;

namespace BuisnessLayer.DBModels
{
    public class tblRole
    {
        public int RoleId { get; set; }
        public string Rolename { get; set; }

        public ICollection<tblUser> tblUsers { get; set; }
    }
}