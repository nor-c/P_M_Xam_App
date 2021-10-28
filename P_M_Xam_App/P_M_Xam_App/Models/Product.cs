using System;
using System.Collections.Generic;
using System.Text;

namespace P_M_Xam_App.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public double Price { get; set; }
    }
}
