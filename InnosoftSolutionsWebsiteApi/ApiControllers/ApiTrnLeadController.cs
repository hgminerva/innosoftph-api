using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    // Router prefix for web api
    [Authorize]
    [RoutePrefix("api/lead")]
    public class ApiTrnLeadController : ApiController
    {
        // database - LinQ to SQL class
        private Data.InnosoftSolutionsDatabaseDataContext db = new Data.InnosoftSolutionsDatabaseDataContext();

        // fill leading zeroes
        public String fillLeadingZeroes(Int32 number, Int32 length)
        {
            var result = number.ToString();
            var pad = length - result.Length;
            while (pad > 0)
            {
                result = '0' + result;
                pad--;
            }

            return result;
        }

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

        // list lead
        [HttpGet, Route("list/byLeadStatus")]
        public List<Entities.TrnLead> listLeadByLeadStatus()
        {
            var leads = from d in db.IS_TrnLeads
                        where d.LeadStatus == "OPEN"
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

        // get lead by id
        [HttpGet, Route("get/byId/{id}")]
        public Entities.TrnLead getLeadById(String id)
        {
            var lead = from d in db.IS_TrnLeads
                       where d.Id == Convert.ToInt32(id)
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

            return (Entities.TrnLead)lead.FirstOrDefault();
        }

        // add lead
        [HttpPost, Route("post")]
        public Int32 postLead(Entities.TrnLead lead)
        {
            try
            {
                // get last lead number
                var lastLeadNumber = from d in db.IS_TrnLeads.OrderByDescending(d => d.Id) select d;
                var leadNumberValue = "0000000001";
                if (lastLeadNumber.Any())
                {
                    var leadNumber = Convert.ToInt32(lastLeadNumber.FirstOrDefault().LeadNumber) + 0000000001;
                    leadNumberValue = fillLeadingZeroes(leadNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnLead newLead = new Data.IS_TrnLead();
                newLead.LeadNumber = leadNumberValue;
                newLead.LeadDate = Convert.ToDateTime(lead.LeadDate);
                newLead.LeadName = lead.LeadName;
                newLead.Address = lead.Address;
                newLead.ContactPerson = lead.ContactPerson;
                newLead.ContactPosition = lead.ContactPosition;
                newLead.ContactEmail = lead.ContactEmail;
                newLead.ContactPhoneNo = lead.ContactPhoneNo;
                newLead.ReferredBy = lead.ReferredBy;
                newLead.Remarks = lead.Remarks;
                newLead.EncodedByUserId = userId;
                newLead.AssignedToUserId = lead.AssignedToUserId;
                newLead.LeadStatus = lead.LeadStatus;
                db.IS_TrnLeads.InsertOnSubmit(newLead);
                db.SubmitChanges();

                return newLead.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update lead
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putLead(String id, Entities.TrnLead lead)
        {
            try
            {
                var leads = from d in db.IS_TrnLeads where d.Id == Convert.ToInt32(id) select d;
                if (leads.Any())
                {
                    var updateLead = leads.FirstOrDefault();
                    updateLead.LeadDate = Convert.ToDateTime(lead.LeadDate);
                    updateLead.LeadName = lead.LeadName;
                    updateLead.Address = lead.Address;
                    updateLead.ContactPerson = lead.ContactPerson;
                    updateLead.ContactPosition = lead.ContactPosition;
                    updateLead.ContactEmail = lead.ContactEmail;
                    updateLead.ContactPhoneNo = lead.ContactPhoneNo;
                    updateLead.ReferredBy = lead.ReferredBy;
                    updateLead.Remarks = lead.Remarks;
                    //updateLead.EncodedByUserId = lead.EncodedByUserId;
                    updateLead.AssignedToUserId = lead.AssignedToUserId;
                    updateLead.LeadStatus = lead.LeadStatus;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // delete lead
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteLead(String id)
        {
            try
            {
                var leads = from d in db.IS_TrnLeads where d.Id == Convert.ToInt32(id) select d;
                if (leads.Any())
                {
                    db.IS_TrnLeads.DeleteOnSubmit(leads.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
