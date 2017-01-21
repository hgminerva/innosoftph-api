using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnLead
    {
        public Int32 Id { get; set; }
        public String LeadNumber { get; set; }
        public String LeadDate { get; set; }
        public String LeadName { get; set; }
        public String Address { get; set; }
        public String ContactPerson { get; set; }
        public String ContactPosition { get; set; }
        public String ContactEmail { get; set; }
        public String ContactPhoneNo { get; set; }
        public String ReferredBy { get; set; }
        public String Remarks { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public Int32? AssignedToUserId { get; set; }
        public String AssignedToUser { get; set; }
        public String LeadStatus { get; set; }
    }
}