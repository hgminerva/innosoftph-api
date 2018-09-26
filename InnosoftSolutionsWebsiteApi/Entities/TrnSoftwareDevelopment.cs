using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnSoftwareDevelopment
    {
        public Int32 Id { get; set; }
        public String SoftDevNumber { get; set; }
        public String SoftDevDate { get; set; }
        public Int32 ProjectId { get; set; }
        public String ProjectNumber { get; set; }
        public String ProjectName { get; set; }
        public String Task { get; set; }
        public String Remarks { get; set; }
        public Decimal NumberOfHours { get; set; }
        public String SoftDevType { get; set; }
        public Decimal Amount { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public Int32? AssignedToUserId { get; set; }
        public String AssignedToUser { get; set; }
        public String SoftDevStatus { get; set; }
    }
}