using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Models
{
    public class DataSoGhi
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public List<SoGhiChiSo> ListSoGhi { get; set; }
    }

    public class ResultSoGhi
    {
        public int status { get; set; }
        public int count { get; set; }
        public string message { get; set; }
    }
}
