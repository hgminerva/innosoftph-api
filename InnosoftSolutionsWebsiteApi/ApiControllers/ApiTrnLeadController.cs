using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    // Router prefix for web api
    [RoutePrefix("api/lead")]
    public class ApiTrnLeadController : ApiController
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // list lead
        [HttpGet, Route("list/byLeadDateRange/{startLeadDate}/{endLeadDate}")]
        public List<Entities.TrnLead> listLeadByLeadDateRange(String startLeadDate, String endLeadDate)
        {
            var leads = from d in db.IS_TrnLeads
                        where d.LeadDate >= Convert.ToDateTime(startLeadDate)
                        && d.LeadDate <= Convert.ToDateTime(endLeadDate)
                        select new Entities.TrnLead
                        {
                            Id = d.Id,
                            LeadNumber = d.LeadNumber,
                            LeadDate = d.LeadDate.ToShortDateString(),
                            LeadName = d.LeadName,
                            Address = d.Address,
                            ContactPerson = d.ContactPerson,
                            ContactPosition = d.ContactPosition,
                            ContactEmail = d.ContactEmail,
                            ContactPhoneNo = d.ContactPhoneNo,
                            ReferredBy = d.ReferredBy,
                            Remarks = d.Remarks,
                            EncodedByUserId = d.EncodedByUserId,
                            EncodedByUser = d.MstUser.FullName,
                            AssignedToUserId = d.AssignedToUserId,
                            AssignedToUser = d.MstUser1.FullName,
                            LeadStatus = d.LeadStatus
                        };

            return leads.ToList();
        }
    }
}
