using System;
using System.Collections.Generic;
using System.Text;

namespace P_M_Xam_App.Models
{
    public class AreaDetails
    {
        public Area Area { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public int CurrencyID { get; set; }
        public int CurID { get; set; }
        public bool RequiresContactDetails { get; set; }
        public bool RequiresVRN { get; set; }
        public bool RequiresMobile { get; set; }
        public bool AllowsUserAccountLogin { get; set; }
        public string TermsAndConditions { get; set; }
        public string Directions { get; set; }
        public string MapURI { get; set; }
        public string FaqURI { get; set; }
        //public Layout Layout { get; set; }
    }
}
