using System;
using System.Collections.Generic;

namespace BuisnessLayer.DBModels
{
    public class tblUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        //foriegn key
        public int RoleId { get; set; }
        public string Nickname { get; set; }
        public string PictureLocation { get; set; }
        public long Points { get; set; }
        public double CartAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        

        public tblRole tblRole { get; set; }
        public ICollection<tblUserOrder> tblUserOrders { get; set; }
    }
}