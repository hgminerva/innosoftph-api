using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class PrintQuotationPaymentLists
    {
        public Int32 Id { get; set; }
        public String Description { get; set; }
        public Decimal Amount { get; set; }
        public String Remarks { get; set; }
    }
}