using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class PrintQuotationObjectLists
    {
        public String CustomerName { get; set; }
        public String CustomerAddress { get; set; }
        public String CustomerContactPerson { get; set; }
        public String CustomerContactNumber { get; set; }
        public String CustomerContactEmail { get; set; }
        public String QRefNumber { get; set; }
        public String QDate { get; set; }
        public String ClientPONo { get; set; }
        public String LeadsRefNo { get; set; }
        public List<Entities.PrintQuotationProductLists> ProdcutLists { get; set; }
        public List<Entities.PrintQuotationPaymentLists> PaymentLists { get; set; }
    }
}