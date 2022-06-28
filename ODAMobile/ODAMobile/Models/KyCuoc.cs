using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Models
{
    [Table("kycuoc")]
    public class KyCuoc : BaseModel
    {
        [Column("id"), PrimaryKey]
        public int ID { get; set; }
        [Column("tenkycuoc")]
        public string TenKyCuoc { get; set; }
    }
}
