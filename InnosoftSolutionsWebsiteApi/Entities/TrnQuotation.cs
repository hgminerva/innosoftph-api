using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnQuotation
    {
        public Int32 Id { get; set; }
        public String QuotationNumber { get; set; }
        public String QuotationDate { get; set; }
        public Int32 LeadId { get; set; }
        public String LeadNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public String Remarks { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public String QuotationStatus { get; set; }

    }
}