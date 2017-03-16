using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class PrintQuotationTimelineLists
    {
        public Int32 Id { get; set; }
        public String Product { get; set; }
        public String Timeline { get; set; }
        public String Remarks { get; set; }
    }
}