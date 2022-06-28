using SQLite;
using System;
using System.Collections.Generic;

namespace ODAMobile.Models
{
    [Table("soghichiso")]
    public class SoGhiChiSo : BaseModel
    {
        [Column("mahopdong"), PrimaryKey]
        public string MaHopDong { get; set; }
        [Column("id_hopdong")]
        public int id_hopdong { get; set; }
        [Column("hoten")]
        public string HoTen { get; set; }
        [Column("diachi")]
        public string DiaChi { get; set; }
        [Column("chisokytruoc")]
        public int ChiSoKyTruoc { get; set; }
        [Column("chisoghiduoc")]
        public int ChiSoGhiDuoc { get; set; }
        [Column("soghiid")]
        public int SoGhiID { get; set; }
        [Column("tenkycuoc")]
        public string TenKyCuoc { get; set; }
        [Column("isupdated")]
        public bool IsUpdated { get; set; }
        [Column("ngayghithucte")]
        public DateTime NgayGhiThucTe { get; set; }
    }

    public class DataSync
    {
        public int NguoiDungID { get; set; }
        public List<SoGhiChiSo> ListSoGhi { get; set; }
    }
}
