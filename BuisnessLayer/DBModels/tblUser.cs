namespace BuisnessLayer.DBModels
{
    public class tblUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string PictureLocation { get; set; }

        //foriegn key
        public int RoleId { get; set; }

        public tblRole tblRole { get; set; }
    }
}