using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    // Router prefix for web api
    [RoutePrefix("api/activity")]
    public class ApiTrnActivityController : ApiController
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

        // get activity by lead id
        [HttpGet, Route("list/byLeadId/{leadId}")]
        public List<Entities.TrnActivity> listActivityByLeadId(String leadId)
        {
            var activities = from d in db.IS_TrnActivities
                             where d.LeadId == Convert.ToInt32(leadId)
                             select new Entities.TrnActivity
                             {
                                 Id = d.Id,
                                 ActivityNumber = d.ActivityNumber,
                                 ActivityDate = d.ActivityDate.ToShortDateString(),
                                 StaffUserId = d.StaffUserId,
                                 StaffUser = d.MstUser.FullName,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 ParticularCategory = d.ParticularCategory,
                                 Particulars = d.Particulars,
                                 NumberOfHours = d.NumberOfHours,
                                 ActivityAmount = d.ActivityAmount,
                                 ActivityStatus = d.ActivityStatus,
                                 LeadId = d.LeadId,
                                 QuotationId = d.QuotationId,
                                 DeliveryId = d.DeliveryId,
                                 SupportId = d.SupportId
                             };

            return activities.ToList();
        }

        // get activity by quotation id
        [HttpGet, Route("list/byQuotationId/{quotationId}")]
        public List<Entities.TrnActivity> listActivityByQuotationId(String quotationId)
        {
            var activities = from d in db.IS_TrnActivities
                             where d.QuotationId == Convert.ToInt32(quotationId)
                             select new Entities.TrnActivity
                             {
                                 Id = d.Id,
                                 ActivityNumber = d.ActivityNumber,
                                 ActivityDate = d.ActivityDate.ToShortDateString(),
                                 StaffUserId = d.StaffUserId,
                                 StaffUser = d.MstUser.FullName,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 ParticularCategory = d.ParticularCategory,
                                 Particulars = d.Particulars,
                                 NumberOfHours = d.NumberOfHours,
                                 ActivityAmount = d.ActivityAmount,
                                 ActivityStatus = d.ActivityStatus,
                                 LeadId = d.LeadId,
                                 QuotationId = d.QuotationId,
                                 DeliveryId = d.DeliveryId,
                                 SupportId = d.SupportId
                             };

            return activities.ToList();
        }

        // get activity by delivery id
        [HttpGet, Route("list/byDeliveryId/{deliveryId}")]
        public List<Entities.TrnActivity> listActivityByDeliveryId(String deliveryId)
        {
            var activities = from d in db.IS_TrnActivities
                             where d.DeliveryId == Convert.ToInt32(deliveryId)
                             select new Entities.TrnActivity
                             {
                                 Id = d.Id,
                                 ActivityNumber = d.ActivityNumber,
                                 ActivityDate = d.ActivityDate.ToShortDateString(),
                                 StaffUserId = d.StaffUserId,
                                 StaffUser = d.MstUser.FullName,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 ParticularCategory = d.ParticularCategory,
                                 Particulars = d.Particulars,
                                 NumberOfHours = d.NumberOfHours,
                                 ActivityAmount = d.ActivityAmount,
                                 ActivityStatus = d.ActivityStatus,
                                 LeadId = d.LeadId,
                                 QuotationId = d.QuotationId,
                                 DeliveryId = d.DeliveryId,
                                 SupportId = d.SupportId
                             };

            return activities.ToList();
        }

        // get activity by support id
        [HttpGet, Route("list/bySupportId/{supportId}")]
        public List<Entities.TrnActivity> listActivityBySupportId(String supportId)
        {
            var activities = from d in db.IS_TrnActivities
                             where d.SupportId == Convert.ToInt32(supportId)
                             select new Entities.TrnActivity
                             {
                                 Id = d.Id,
                                 ActivityNumber = d.ActivityNumber,
                                 ActivityDate = d.ActivityDate.ToShortDateString(),
                                 StaffUserId = d.StaffUserId,
                                 StaffUser = d.MstUser.FullName,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 ParticularCategory = d.ParticularCategory,
                                 Particulars = d.Particulars,
                                 NumberOfHours = d.NumberOfHours,
                                 ActivityAmount = d.ActivityAmount,
                                 ActivityStatus = d.ActivityStatus,
                                 LeadId = d.LeadId,
                                 QuotationId = d.QuotationId,
                                 DeliveryId = d.DeliveryId,
                                 SupportId = d.SupportId
                             };

            return activities.ToList();
        }

        // get latest activity date
        public String getActivityDate(String documentName, Int32 documentId)
        {
            if (documentName.Equals("Lead"))
            {
                var leadActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                   where d.LeadId == Convert.ToInt32(documentId)
                                   select d;

                if (leadActivity.Any())
                {
                    return leadActivity.FirstOrDefault().ActivityDate.ToShortDateString();
                }
                else
                {
                    return " ";
                }
            }
            else
            {
                if (documentName.Equals("Quotation"))
                {
                    var quotationActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                            where d.QuotationId == Convert.ToInt32(documentId)
                                            select d;

                    if (quotationActivity.Any())
                    {
                        return quotationActivity.FirstOrDefault().ActivityDate.ToShortDateString();
                    }
                    else
                    {
                        return " ";
                    }
                }
                else
                {
                    if (documentName.Equals("Delivery"))
                    {
                        var deliveryActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                               where d.DeliveryId == Convert.ToInt32(documentId)
                                               select d;

                        if (deliveryActivity.Any())
                        {
                            return deliveryActivity.FirstOrDefault().ActivityDate.ToShortDateString();
                        }
                        else
                        {
                            return " ";
                        }
                    }
                    else
                    {
                        if (documentName.Equals("Support"))
                        {
                            var supportActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                                  where d.SupportId == Convert.ToInt32(documentId)
                                                  select d;

                            if (supportActivity.Any())
                            {
                                return supportActivity.FirstOrDefault().ActivityDate.ToShortDateString();
                            }
                            else
                            {
                                return " ";
                            }
                        }
                        else
                        {
                            return " ";
                        }
                    }
                }
            }
        }

        // get latest activity
        public String getActivityParticulars(String documentName, Int32 documentId)
        {
            if (documentName.Equals("Lead"))
            {
                var leadActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                   where d.LeadId == Convert.ToInt32(documentId)
                                   select d;

                if (leadActivity.Any())
                {
                    return leadActivity.FirstOrDefault().Particulars;
                }
                else
                {
                    return " ";
                }
            }
            else
            {
                if (documentName.Equals("Quotation"))
                {
                    var quotationActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                            where d.QuotationId == Convert.ToInt32(documentId)
                                            select d;

                    if (quotationActivity.Any())
                    {
                        return quotationActivity.FirstOrDefault().Particulars;
                    }
                    else
                    {
                        return " ";
                    }
                }
                else
                {
                    if (documentName.Equals("Delivery"))
                    {
                        var deliveryActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                               where d.DeliveryId == Convert.ToInt32(documentId)
                                               select d;

                        if (deliveryActivity.Any())
                        {
                            return deliveryActivity.FirstOrDefault().Particulars;
                        }
                        else
                        {
                            return " ";
                        }
                    }
                    else
                    {
                        if (documentName.Equals("Support"))
                        {
                            var supportActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                                  where d.SupportId == Convert.ToInt32(documentId)
                                                  select d;

                            if (supportActivity.Any())
                            {
                                return supportActivity.FirstOrDefault().Particulars;
                            }
                            else
                            {
                                return " ";
                            }
                        }
                        else
                        {
                            return " ";
                        }
                    }
                }
            }
        }

        // get latest activity staff
        public String getActivityStaffUser(String documentName, Int32 documentId)
        {
            if (documentName.Equals("Lead"))
            {
                var leadActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                   where d.LeadId == Convert.ToInt32(documentId)
                                   select d;

                if (leadActivity.Any())
                {
                    return leadActivity.FirstOrDefault().MstUser.FullName;
                }
                else
                {
                    return " ";
                }
            }
            else
            {
                if (documentName.Equals("Quotation"))
                {
                    var quotationActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                            where d.QuotationId == Convert.ToInt32(documentId)
                                            select d;

                    if (quotationActivity.Any())
                    {
                        return quotationActivity.FirstOrDefault().MstUser.FullName;
                    }
                    else
                    {
                        return " ";
                    }
                }
                else
                {
                    if (documentName.Equals("Delivery"))
                    {
                        var deliveryActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                               where d.DeliveryId == Convert.ToInt32(documentId)
                                               select d;

                        if (deliveryActivity.Any())
                        {
                            return deliveryActivity.FirstOrDefault().MstUser.FullName;
                        }
                        else
                        {
                            return " ";
                        }
                    }
                    else
                    {
                        if (documentName.Equals("Support"))
                        {
                            var supportActivity = from d in db.IS_TrnActivities.OrderByDescending(d => d.ActivityDate)
                                                  where d.SupportId == Convert.ToInt32(documentId)
                                                  select d;

                            if (supportActivity.Any())
                            {
                                return supportActivity.FirstOrDefault().MstUser.FullName;
                            }
                            else
                            {
                                return " ";
                            }
                        }
                        else
                        {
                            return " ";
                        }
                    }
                }
            }
        }

        // get document with latest activity by document reference and by date ranged
        [HttpGet, Route("list/byDocument/byDateRanged/{document}/{startDate}/{endDate}")]
        public List<Entities.TrnActivity> listActivityByDocumentByDateRanged(String document, String startDate, String endDate)
        {
            if (document.Equals("Lead"))
            {
                var leads = from d in db.IS_TrnLeads
                            where d.LeadDate >= Convert.ToDateTime(startDate)
                            && d.LeadDate <= Convert.ToDateTime(endDate)
                            select new Entities.TrnActivity
                            {
                                Id = d.Id,
                                DocumentNumber = "LN - " + d.LeadNumber,
                                ActivityDate = getActivityDate("Lead", d.Id),
                                Particulars = d.LeadName + " - " + d.Remarks,
                                Activity = getActivityParticulars("Lead", d.Id),
                                StaffUser = getActivityStaffUser("Lead", d.Id),
                            };

                return leads.ToList();
            }
            else
            {
                if (document.Equals("Quotation"))
                {
                    var quotations = from d in db.IS_TrnQuotations
                                     where d.QuotationDate >= Convert.ToDateTime(startDate)
                                     && d.QuotationDate <= Convert.ToDateTime(endDate)
                                     select new Entities.TrnActivity
                                     {
                                         Id = d.Id,
                                         DocumentNumber = "QN - " + d.QuotationNumber,
                                         ActivityDate = getActivityDate("Quotation", d.Id),
                                         Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                         Activity = getActivityParticulars("Quotation", d.Id),
                                         StaffUser = getActivityStaffUser("Quotation", d.Id),
                                     };

                    return quotations.ToList();
                }
                else
                {
                    if (document.Equals("Delivery"))
                    {
                        var deliveries = from d in db.IS_TrnDeliveries
                                         where d.DeliveryDate >= Convert.ToDateTime(startDate)
                                         && d.DeliveryDate <= Convert.ToDateTime(endDate)
                                         select new Entities.TrnActivity
                                         {
                                             Id = d.Id,
                                             DocumentNumber = "DN - " + d.DeliveryNumber,
                                             ActivityDate = getActivityDate("Delivery", d.Id),
                                             Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                             Activity = getActivityParticulars("Delivery", d.Id),
                                             StaffUser = getActivityStaffUser("Delivery", d.Id),
                                         };

                        return deliveries.ToList();
                    }
                    else
                    {
                        if (document.Equals("Support"))
                        {
                            var supports = from d in db.IS_TrnSupports
                                           where d.SupportDate >= Convert.ToDateTime(startDate)
                                           && d.SupportDate <= Convert.ToDateTime(endDate)
                                           select new Entities.TrnActivity
                                           {
                                               Id = d.Id,
                                               DocumentNumber = "SN - " + d.SupportDate,
                                               ActivityDate = getActivityDate("Support", d.Id),
                                               Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                               Activity = getActivityParticulars("Support", d.Id),
                                               StaffUser = getActivityStaffUser("Support", d.Id),
                                           };

                            return supports.ToList();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        // add activity
        [HttpPost, Route("post")]
        public HttpResponseMessage postActivity(Entities.TrnActivity activity)
        {
            try
            {
                // get last activity number
                var lastActivityNumber = from d in db.IS_TrnActivities.OrderByDescending(d => d.Id) select d;
                var activityNumberValue = "0000000001";
                if (lastActivityNumber.Any())
                {
                    var activityNumber = Convert.ToInt32(lastActivityNumber.FirstOrDefault().ActivityNumber) + 0000000001;
                    activityNumberValue = fillLeadingZeroes(activityNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnActivity newActivity = new Data.IS_TrnActivity();
                newActivity.ActivityNumber = activityNumberValue;
                newActivity.ActivityDate = Convert.ToDateTime(activity.ActivityDate);
                newActivity.StaffUserId = userId;
                newActivity.CustomerId = activity.CustomerId;
                newActivity.ProductId = activity.ProductId;
                newActivity.ParticularCategory = activity.ParticularCategory;
                newActivity.Particulars = activity.Particulars;
                newActivity.NumberOfHours = activity.NumberOfHours;
                newActivity.ActivityAmount = activity.ActivityAmount;
                newActivity.ActivityStatus = activity.ActivityStatus;
                newActivity.LeadId = activity.LeadId;
                newActivity.QuotationId = activity.QuotationId;
                newActivity.DeliveryId = activity.DeliveryId;
                newActivity.SupportId = activity.SupportId;
                db.IS_TrnActivities.InsertOnSubmit(newActivity);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update activity
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putActivity(String id, Entities.TrnActivity activity)
        {
            try
            {
                var activities = from d in db.IS_TrnActivities where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    var updateActivity = activities.FirstOrDefault();
                    updateActivity.ActivityDate = Convert.ToDateTime(activity.ActivityDate);
                    //updateActivity.StaffUserId = activity.StaffUserId;
                    updateActivity.CustomerId = activity.CustomerId;
                    updateActivity.ProductId = activity.ProductId;
                    updateActivity.ParticularCategory = activity.ParticularCategory;
                    updateActivity.Particulars = activity.Particulars;
                    updateActivity.NumberOfHours = activity.NumberOfHours;
                    updateActivity.ActivityAmount = activity.ActivityAmount;
                    updateActivity.ActivityStatus = activity.ActivityStatus;
                    updateActivity.LeadId = activity.LeadId;
                    updateActivity.QuotationId = activity.QuotationId;
                    updateActivity.DeliveryId = activity.DeliveryId;
                    updateActivity.SupportId = activity.SupportId;
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

        // delete activity
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteActivity(String id)
        {
            try
            {
                var activities = from d in db.IS_TrnActivities where d.Id == Convert.ToInt32(id) select d;
                if (activities.Any())
                {
                    db.IS_TrnActivities.DeleteOnSubmit(activities.First());
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
