using System;
using System.Collections.Generic;

#nullable disable

namespace BuisnessLayer.DBModels
{
    public partial class TblMenuType
    {
        public TblMenuType()
        {
            TblMenus = new HashSet<TblMenu>();
        }

        public int MenuTypeId { get; set; }
        public string MenuTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<TblMenu> TblMenus { get; set; }
    }
}
