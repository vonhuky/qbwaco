using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Models
{
    [Table("cauhinh")]
    public class SettingModel : BaseModel
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Column("host")]
        public string host { get; set; }
        [Column("port")]
        public int port { get; set; }
        [Column("autosync")]
        public bool autosync { get; set; }
        [Column("sound")]
        public bool sound { get; set; }
        [Column("vibrate")]
        public bool vibrate { get; set; }
    }
}
