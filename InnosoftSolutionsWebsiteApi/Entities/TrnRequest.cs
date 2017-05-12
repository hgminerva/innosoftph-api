using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnRequest
    {
        public Int32 Id { get; set; }
        public String RequestNumber { get; set; }
        public String RequestDate { get; set; }
        public String RequestType { get; set; }
        public String Particulars { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public Int32? CheckedByUserId { get; set; }
        public String CheckedByUser { get; set; }
        public String CheckedRemarks { get; set; }
        public Int32? ApprovedByUserId { get; set; }
        public String ApprovedByUser { get; set; }
        public String ApprovedRemarks { get; set; }
        public String RequestStatus { get; set; }
    }
}