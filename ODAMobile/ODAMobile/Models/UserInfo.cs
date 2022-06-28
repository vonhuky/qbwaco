using SQLite;

namespace ODAMobile.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class login_message_rs
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ListLoTrinh { get; set; }
        public int Status { get; set; }
    }
}
