using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Models
{
    [Table("contract")]
    public class HopDong : BaseModel
    {
        [Column("id"), PrimaryKey]
        public int ID { get; set; }
        [Column("mahopdong")]
        public string MaHopDong { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }

    public class ResultData
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public int status { get; set; }
        public String message { get; set; }
        public List<ListPost> data_postage { get; set; }
    }

    public class ListPost
    {
        public Postage Postage { get; set; }
        public List<ListPost> data { get; set; }
    }

    
}
