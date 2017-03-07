using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnContinuity
    {
        public Int32 Id { get; set; }
        public String ContinuityNumber { get; set; }
        public String ContinuityDate { get; set; }
        public Int32 DeliveryId { get; set; }
        public String DeliveryNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public String ExpiryDate { get; set; }
        public String Remarks { get; set; }
        public Int32 StaffUserId { get; set; }
        public String StaffUser { get; set; }
        public String ContinuityStatus { get; set; }
    }
}