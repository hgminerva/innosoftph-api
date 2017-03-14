using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class PrintQuotationProductLists
    {
        public Int32 Id { get; set; }
        public String ProductCode { get; set; }
        public String ProductDescription { get; set; }
        public Decimal Price { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Amount { get; set; }
    }
}