using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnosoftSolutionsWebsiteApi.Entities
{
    public class TrnProject
    {
        public Int32 Id { get; set; }
        public String ProjectNumber { get; set; }
        public String ProjectDate { get; set; }
        public String ProjectName { get; set; }
        public String ProjectType { get; set; }
        public Int32 CustomerId { get; set; }
        public String Customer { get; set; }
        public String Particulars { get; set; }
        public Int32 EncodedByUserId { get; set; }
        public String EncodedByUser { get; set; }
        public Int32 ProjectManagerUserId { get; set; }
        public String ProjectManagerUser { get; set; }
        public String ProjectStartDate { get; set; }
        public String ProjectEndDate { get; set; }
        public String ProjectStatus { get; set; }
    }
}