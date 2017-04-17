using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnActivity
    {
        public Int32 Id { get; set; }
        public String ActivityNumber { get; set; }
        public String ActivityDate { get; set; }
        public Int32 StaffUserId { get; set; }
        public String StaffUser { get; set; }
        public Int32? CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32? ProductId { get; set; }
        public String Product { get; set; }
        public String ParticularCategory { get; set; }
        public String Particulars { get; set; }
        public Decimal NumberOfHours { get; set; }
        public Decimal ActivityAmount { get; set; }
        public String ActivityStatus { get; set; }
        public Int32? LeadId { get; set; }
        public Int32? QuotationId { get; set; }
        public Int32? DeliveryId { get; set; }
        public Int32? SupportId { get; set; }
        public Int32? SoftwareDevelopmentId { get; set; }
        public String DocumentNumber { get; set; }
        public String Activity { get; set; }
        public String HeaderStatus { get; set; }
        public String EncodedBy { get; set; }
        public String HeaderRemarks { get; set; }
    }
}