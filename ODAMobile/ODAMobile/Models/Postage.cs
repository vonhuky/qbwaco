using SQLite;

namespace ODAMobile.Models
{
    [Table("postage")]
    public class Postage : BaseModel
    {
        [Column("mahoadon")]
        public string mahoadon { get; set; }
        [Column("id")]
        public int id { get; set; }
        [Column("id_hopdong")]
        public int id_hopdong { get; set; }
        [Column("id_hoadon"), PrimaryKey]
        public int id_hoadon { get; set; }
        [Column("thang_cuoc")]
        public string thang_cuoc { get; set; }
        [Column("id_kycuoc")]
        public int id_kycuoc { get; set; }
        [Column("ma_ttoan")]
        public string ma_ttoan { get; set; }
        [Column("sm_daidien")]
        public string sm_daidien { get; set; }
        [Column("ten_kh")]
        public string ten_kh { get; set; }
        [Column("diachi_kh")]
        public string diachi_kh { get; set; }
        [Column("t_duong")]
        public int? t_duong { get; set; }
        [Column("tong_ps")]
        public double tong_ps { get; set; }
        [Column("thuong_gb")]
        public int thuong_gb { get; set; }
        [Column("vat_vn")]
        public double vat_vn { get; set; }
        [Column("giam_tru")]
        public int giam_tru { get; set; }
        [Column("no_vn")]
        public int no_vn { get; set; }
        [Column("tong_cong")]
        public int? tong_cong { get; set; }
        [Column("phat_sinh")]
        public int? phat_sinh { get; set; }
        [Column("doi_tuong")]
        public int doi_tuong { get; set; }
        [Column("ma_ctv")]
        public int ma_ctv { get; set; }
        [Column("stt")]
        public string stt { get; set; }
        [Column("sm_lienhe")]
        public string sm_lienhe { get; set; }
        [Column("email_lienhe")]
        public string email_lienhe { get; set; }
        [Column("thu_duoc")]
        public int? thu_duoc { get; set; }
        [Column("ma_vach")]
        public string ma_vach { get; set; }
        [Column("ghichu")]
        public string ghichu { get; set; }
        [Column("phieu_bao")]
        public int phieu_bao { get; set; }
        [Column("phieu_thu")]
        public int phieu_thu { get; set; }
        [Column("status")]
        public int? status { get; set; }
        [Column("ThuNganID")]
        public int ThuNganID { get; set; }
        [Column("ChiSoGhiDuoc")]
        public int ChiSoGhiDuoc { get; set; }
        [Column("ChiSoKyTruoc")]
        public int ChiSoKyTruoc { get; set; }
        [Column("TrangThai")]
        public int TrangThai { get; set; }
        [Column("SoGhiChiSoID")]
        public int SoGhiChiSoID { get; set; }
        [Column("NgayGhiThucTe")]
        public string NgayGhiThucTe { get; set; }       
    }
}
