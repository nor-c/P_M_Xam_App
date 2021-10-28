using System;
using System.Collections.Generic;
using System.Text;

namespace P_M_Xam_App.Models
{
    public class ProductsSelect
    {
        public int AreaID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string PromoCode { get; set; }
        public List<string> References { get; set; }
    }
}
