using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnSupport
    {
        public Int32 Id { get; set; }
        public String SupportNumber { get; set; }
        public String SupportDate { get; set; }
        public Int32 ContinuityId { get; set; }
        public String ContinuityNumber { get; set; }
        public String IssueCategory { get; set; }
        public String Issue { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public Int32 ProductId { get; set; }
        public String Product { get; set; }
        public String Severity { get; set; }
        public String Caller { get; set; }
        public String Remarks { get; set; }
        public String ScreenShotURL { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public Int32? AssignedToUserId { get; set; }
        public String AssignedToUser { get; set; }
        public String SupportStatus { get; set; }
    }
}