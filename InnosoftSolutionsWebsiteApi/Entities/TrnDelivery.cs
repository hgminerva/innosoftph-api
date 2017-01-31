using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnDelivery
    {
        public Int32 Id { get; set; }
        public String DeliveryNumber { get; set; }
        public String DeliveryDate { get; set; }
        public Int32 QuotationId { get; set; }
        public String QuotationNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public String MeetingDate { get; set; }
        public String Remarks { get; set; }
        public Int32 SalesUserId { get; set; }
        public String SalesUser { get; set; }
        public Int32 TechnicalUserId { get; set; }
        public String TechnicalUser { get; set; }
        public Int32 FunctionalUserId { get; set; }
        public String FunctionalUser { get; set; }
        public String DeliveryStatus { get; set; }
    }
}