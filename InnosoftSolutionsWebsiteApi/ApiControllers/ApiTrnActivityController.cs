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
                                 SupportId = d.SupportId,
                                 SoftwareDevelopmentId = d.SoftwareDevelopmentId
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
                                 SupportId = d.SupportId,
                                 SoftwareDevelopmentId = d.SoftwareDevelopmentId
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
                                 SupportId = d.SupportId,
                                 SoftwareDevelopmentId = d.SoftwareDevelopmentId
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
                                 SupportId = d.SupportId,
                                 SoftwareDevelopmentId = d.SoftwareDevelopmentId
                             };

            return activities.ToList();
        }

        // get activity by software developement id
        [HttpGet, Route("list/bySoftwareDevelopmentId/{softwareDevelopmentId}")]
        public List<Entities.TrnActivity> listActivityBySoftwareDevelopmentId(String softwareDevelopmentId)
        {
            var activities = from d in db.IS_TrnActivities
                             where d.SoftwareDevelopmentId == Convert.ToInt32(softwareDevelopmentId)
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
                                 SupportId = d.SupportId,
                                 SoftwareDevelopmentId = d.SoftwareDevelopmentId
                             };

            return activities.ToList();
        }

        // get document with latest activity by document reference and by date ranged
        [HttpGet, Route("list/byDocument/byDateRanged/{document}/{startDate}/{endDate}/{status}")]
        public List<Entities.TrnActivity> listActivityByDocumentByDateRanged(String document, String startDate, String endDate, String status)
        {
            if (status.Equals("ALL"))
            {
                if (document.Equals("Lead"))
                {
                    var leads = from d in db.IS_TrnLeads
                                join s in db.IS_TrnActivities
                                on d.Id equals s.LeadId
                                into joinActivities
                                from activities in joinActivities.DefaultIfEmpty()
                                where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.LeadDate >= Convert.ToDateTime(startDate))
                                && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.LeadDate <= Convert.ToDateTime(endDate))
                                && (activities.ActivityDate == null ? d.LeadDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                select new Entities.TrnActivity
                                {
                                    Id = activities.Id == null ? 0 : activities.Id,
                                    DocumentNumber = "LN - " + d.LeadNumber,
                                    ActivityDate = activities.ActivityDate == null ? d.LeadDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                    Particulars = d.LeadName + " - " + d.Remarks,
                                    Activity = activities.Particulars == null ? " " : activities.Particulars,
                                    StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                    StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                    LeadId = d.Id,
                                    QuotationId = null,
                                    DeliveryId = null,
                                    SupportId = null,
                                    SoftwareDevelopmentId = null,
                                    CustomerId = null,
                                    ProductId = null,
                                    ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                    NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                    ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                    ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                    HeaderStatus = d.LeadStatus,
                                    EncodedBy = d.MstUser.FullName
                                };

                    return leads.ToList();
                }
                else
                {
                    if (document.Equals("Quotation"))
                    {
                        var quotations = from d in db.IS_TrnQuotations
                                         join s in db.IS_TrnActivities
                                         on d.Id equals s.QuotationId
                                         into joinActivities
                                         from activities in joinActivities.DefaultIfEmpty()
                                         where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.QuotationDate >= Convert.ToDateTime(startDate))
                                         && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.QuotationDate <= Convert.ToDateTime(endDate))
                                         && (activities.ActivityDate == null ? d.QuotationDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                         select new Entities.TrnActivity
                                         {
                                             Id = activities.Id == null ? 0 : activities.Id,
                                             DocumentNumber = "QN - " + d.QuotationNumber,
                                             ActivityDate = activities.ActivityDate == null ? d.QuotationDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                             Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                             Activity = activities.Particulars == null ? " " : activities.Particulars,
                                             StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                             StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                             LeadId = null,
                                             QuotationId = d.Id,
                                             DeliveryId = null,
                                             SupportId = null,
                                             SoftwareDevelopmentId = null,
                                             CustomerId = d.CustomerId,
                                             ProductId = d.ProductId,
                                             ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                             NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                             ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                             ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                             HeaderStatus = d.QuotationStatus,
                                             EncodedBy = d.MstUser.FullName
                                         };

                        return quotations.ToList();
                    }
                    else
                    {
                        if (document.Equals("Delivery"))
                        {
                            var deliveries = from d in db.IS_TrnDeliveries
                                             join s in db.IS_TrnActivities
                                             on d.Id equals s.DeliveryId
                                             into joinActivities
                                             from activities in joinActivities.DefaultIfEmpty()
                                             where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.DeliveryDate >= Convert.ToDateTime(startDate))
                                             && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.DeliveryDate <= Convert.ToDateTime(endDate))
                                             && (activities.ActivityDate == null ? d.DeliveryDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                             select new Entities.TrnActivity
                                             {
                                                 Id = activities.Id == null ? 0 : activities.Id,
                                                 DocumentNumber = "DN - " + d.DeliveryNumber,
                                                 ActivityDate = activities.ActivityDate == null ? d.DeliveryDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                 Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                 Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                 StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                 StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                 LeadId = null,
                                                 QuotationId = null,
                                                 DeliveryId = d.Id,
                                                 SupportId = null,
                                                 SoftwareDevelopmentId = null,
                                                 CustomerId = d.CustomerId,
                                                 ProductId = d.ProductId,
                                                 ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                 NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                 ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                 ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                 HeaderStatus = d.DeliveryStatus,
                                                 EncodedBy = d.MstUser.FullName
                                             };

                            return deliveries.ToList();
                        }
                        else
                        {
                            if (document.Equals("Support"))
                            {
                                var supports = from d in db.IS_TrnSupports
                                               join s in db.IS_TrnActivities
                                               on d.Id equals s.SupportId
                                               into joinActivities
                                               from activities in joinActivities.DefaultIfEmpty()
                                               where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                               && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                               && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                               select new Entities.TrnActivity
                                               {
                                                   Id = activities.Id == null ? 0 : activities.Id,
                                                   DocumentNumber = "SN - " + d.SupportNumber,
                                                   ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                   Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                   Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                   StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                   StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                   LeadId = null,
                                                   QuotationId = null,
                                                   DeliveryId = null,
                                                   SupportId = d.Id,
                                                   SoftwareDevelopmentId = null,
                                                   CustomerId = d.CustomerId,
                                                   ProductId = d.ProductId,
                                                   ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                   NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                   ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                   ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                   HeaderStatus = d.SupportStatus,
                                                   EncodedBy = d.MstUser.FullName
                                               };

                                return supports.ToList();
                            }
                            else
                            {
                                if (document.Equals("Software Development"))
                                {
                                    var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments
                                                               join s in db.IS_TrnActivities
                                                               on d.Id equals s.SoftwareDevelopmentId
                                                               into joinActivities
                                                               from activities in joinActivities.DefaultIfEmpty()
                                                               where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SoftDevDate >= Convert.ToDateTime(startDate))
                                                               && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SoftDevDate <= Convert.ToDateTime(endDate))
                                                               && (activities.ActivityDate == null ? d.SoftDevDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                               select new Entities.TrnActivity
                                                               {
                                                                   Id = activities.Id == null ? 0 : activities.Id,
                                                                   DocumentNumber = "SD - " + d.SoftDevNumber,
                                                                   ActivityDate = activities.ActivityDate == null ? d.SoftDevDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                                   Particulars = d.Task + " - " + d.Remarks,
                                                                   Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                                   StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                                   StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                                   LeadId = null,
                                                                   QuotationId = null,
                                                                   DeliveryId = null,
                                                                   SupportId = null,
                                                                   SoftwareDevelopmentId = d.Id,
                                                                   CustomerId = null,
                                                                   ProductId = null,
                                                                   ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                                   NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                                   ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                                   ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                                   HeaderStatus = d.SoftDevStatus,
                                                                   EncodedBy = d.MstUser.FullName
                                                               };

                                    return softwareDevelopments.ToList();
                                }
                                else
                                {
                                    if (document.Equals("Support - Technical"))
                                    {
                                        var supports = from d in db.IS_TrnSupports
                                                       join s in db.IS_TrnActivities
                                                       on d.Id equals s.SupportId
                                                       into joinActivities
                                                       from activities in joinActivities.DefaultIfEmpty()
                                                       where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                                       && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                                       && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                       && d.SupportType == "Technical"
                                                       select new Entities.TrnActivity
                                                       {
                                                           Id = activities.Id == null ? 0 : activities.Id,
                                                           DocumentNumber = "SN - " + d.SupportNumber,
                                                           ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                           Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                           Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                           StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                           StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                           LeadId = null,
                                                           QuotationId = null,
                                                           DeliveryId = null,
                                                           SupportId = d.Id,
                                                           SoftwareDevelopmentId = null,
                                                           CustomerId = d.CustomerId,
                                                           ProductId = d.ProductId,
                                                           ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                           NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                           ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                           ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                           HeaderStatus = d.SupportStatus,
                                                           EncodedBy = d.MstUser.FullName
                                                       };

                                        return supports.ToList();
                                    }
                                    else
                                    {
                                        if (document.Equals("Support - Functional"))
                                        {
                                            var supports = from d in db.IS_TrnSupports
                                                           join s in db.IS_TrnActivities
                                                           on d.Id equals s.SupportId
                                                           into joinActivities
                                                           from activities in joinActivities.DefaultIfEmpty()
                                                           where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                                           && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                                           && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                           && d.SupportType == "Functional"
                                                           select new Entities.TrnActivity
                                                           {
                                                               Id = activities.Id == null ? 0 : activities.Id,
                                                               DocumentNumber = "SN - " + d.SupportNumber,
                                                               ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                               Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                               Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                               StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                               StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                               LeadId = null,
                                                               QuotationId = null,
                                                               DeliveryId = null,
                                                               SupportId = d.Id,
                                                               SoftwareDevelopmentId = null,
                                                               CustomerId = d.CustomerId,
                                                               ProductId = d.ProductId,
                                                               ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                               NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                               ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                               ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                               HeaderStatus = d.SupportStatus,
                                                               EncodedBy = d.MstUser.FullName
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
                    }
                }
            }
            else
            {
                String documentStatus = "OPEN";
                if (status.Equals("CLOSE"))
                {
                    documentStatus = "CLOSE";
                }
                else
                {
                    if (status.Equals("WAITING FOR CLIENT"))
                    {
                        documentStatus = "WAITING FOR CLIENT";
                    }
                    else
                    {
                        if (status.Equals("CANCELLED"))
                        {
                            documentStatus = "CANCELLED";
                        }
                        else
                        {
                            if (status.Equals("DONE"))
                            {
                                documentStatus = "DONE";
                            }
                        }
                    }
                }

                if (document.Equals("Lead"))
                {
                    var leads = from d in db.IS_TrnLeads
                                join s in db.IS_TrnActivities
                                on d.Id equals s.LeadId
                                into joinActivities
                                from activities in joinActivities.DefaultIfEmpty()
                                where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.LeadDate >= Convert.ToDateTime(startDate))
                                && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.LeadDate <= Convert.ToDateTime(endDate))
                                && (activities.ActivityDate == null ? d.LeadDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                && d.LeadStatus == documentStatus
                                select new Entities.TrnActivity
                                {
                                    Id = activities.Id == null ? 0 : activities.Id,
                                    DocumentNumber = "LN - " + d.LeadNumber,
                                    ActivityDate = activities.ActivityDate == null ? d.LeadDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                    Particulars = d.LeadName + " - " + d.Remarks,
                                    Activity = activities.Particulars == null ? " " : activities.Particulars,
                                    StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                    StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                    LeadId = d.Id,
                                    QuotationId = null,
                                    DeliveryId = null,
                                    SupportId = null,
                                    SoftwareDevelopmentId = null,
                                    CustomerId = null,
                                    ProductId = null,
                                    ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                    NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                    ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                    ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                    HeaderStatus = d.LeadStatus,
                                    EncodedBy = d.MstUser.FullName
                                };

                    return leads.ToList();
                }
                else
                {
                    if (document.Equals("Quotation"))
                    {
                        var quotations = from d in db.IS_TrnQuotations
                                         join s in db.IS_TrnActivities
                                         on d.Id equals s.QuotationId
                                         into joinActivities
                                         from activities in joinActivities.DefaultIfEmpty()
                                         where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.QuotationDate >= Convert.ToDateTime(startDate))
                                         && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.QuotationDate <= Convert.ToDateTime(endDate))
                                         && (activities.ActivityDate == null ? d.QuotationDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                         && d.QuotationStatus == documentStatus
                                         select new Entities.TrnActivity
                                         {
                                             Id = activities.Id == null ? 0 : activities.Id,
                                             DocumentNumber = "QN - " + d.QuotationNumber,
                                             ActivityDate = activities.ActivityDate == null ? d.QuotationDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                             Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                             Activity = activities.Particulars == null ? " " : activities.Particulars,
                                             StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                             StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                             LeadId = null,
                                             QuotationId = d.Id,
                                             DeliveryId = null,
                                             SupportId = null,
                                             SoftwareDevelopmentId = null,
                                             CustomerId = d.CustomerId,
                                             ProductId = d.ProductId,
                                             ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                             NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                             ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                             ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                             HeaderStatus = d.QuotationStatus,
                                             EncodedBy = d.MstUser.FullName
                                         };

                        return quotations.ToList();
                    }
                    else
                    {
                        if (document.Equals("Delivery"))
                        {
                            var deliveries = from d in db.IS_TrnDeliveries
                                             join s in db.IS_TrnActivities
                                             on d.Id equals s.DeliveryId
                                             into joinActivities
                                             from activities in joinActivities.DefaultIfEmpty()
                                             where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.DeliveryDate >= Convert.ToDateTime(startDate))
                                             && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.DeliveryDate <= Convert.ToDateTime(endDate))
                                             && (activities.ActivityDate == null ? d.DeliveryDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                             && d.DeliveryStatus == documentStatus
                                             select new Entities.TrnActivity
                                             {
                                                 Id = activities.Id == null ? 0 : activities.Id,
                                                 DocumentNumber = "DN - " + d.DeliveryNumber,
                                                 ActivityDate = activities.ActivityDate == null ? d.DeliveryDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                 Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                 Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                 StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                 StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                 LeadId = null,
                                                 QuotationId = null,
                                                 DeliveryId = d.Id,
                                                 SupportId = null,
                                                 SoftwareDevelopmentId = null,
                                                 CustomerId = d.CustomerId,
                                                 ProductId = d.ProductId,
                                                 ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                 NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                 ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                 ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                 HeaderStatus = d.DeliveryStatus,
                                                 EncodedBy = d.MstUser.FullName
                                             };

                            return deliveries.ToList();
                        }
                        else
                        {
                            if (document.Equals("Support"))
                            {
                                var supports = from d in db.IS_TrnSupports
                                               join s in db.IS_TrnActivities
                                               on d.Id equals s.SupportId
                                               into joinActivities
                                               from activities in joinActivities.DefaultIfEmpty()
                                               where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                               && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                               && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                               && d.SupportStatus == documentStatus
                                               select new Entities.TrnActivity
                                               {
                                                   Id = activities.Id == null ? 0 : activities.Id,
                                                   DocumentNumber = "SN - " + d.SupportNumber,
                                                   ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                   Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                   Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                   StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                   StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                   LeadId = null,
                                                   QuotationId = null,
                                                   DeliveryId = null,
                                                   SupportId = d.Id,
                                                   SoftwareDevelopmentId = null,
                                                   CustomerId = d.CustomerId,
                                                   ProductId = d.ProductId,
                                                   ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                   NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                   ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                   ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                   HeaderStatus = d.SupportStatus,
                                                   EncodedBy = d.MstUser.FullName
                                               };

                                return supports.ToList();
                            }
                            else
                            {
                                if (document.Equals("Software Development"))
                                {
                                    var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments
                                                               join s in db.IS_TrnActivities
                                                               on d.Id equals s.SoftwareDevelopmentId
                                                               into joinActivities
                                                               from activities in joinActivities.DefaultIfEmpty()
                                                               where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SoftDevDate >= Convert.ToDateTime(startDate))
                                                               && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SoftDevDate <= Convert.ToDateTime(endDate))
                                                               && (activities.ActivityDate == null ? d.SoftDevDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                               && d.SoftDevStatus == documentStatus
                                                               select new Entities.TrnActivity
                                                               {
                                                                   Id = activities.Id == null ? 0 : activities.Id,
                                                                   DocumentNumber = "SD - " + d.SoftDevNumber,
                                                                   ActivityDate = activities.ActivityDate == null ? d.SoftDevDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                                   Particulars = d.Task + " - " + d.Remarks,
                                                                   Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                                   StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                                   StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                                   LeadId = null,
                                                                   QuotationId = null,
                                                                   DeliveryId = null,
                                                                   SupportId = null,
                                                                   SoftwareDevelopmentId = d.Id,
                                                                   CustomerId = null,
                                                                   ProductId = null,
                                                                   ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                                   NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                                   ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                                   ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                                   HeaderStatus = d.SoftDevStatus,
                                                                   EncodedBy = d.MstUser.FullName
                                                               };

                                    return softwareDevelopments.ToList();
                                }
                                else
                                {
                                    if (document.Equals("Support - Technical"))
                                    {
                                        var supports = from d in db.IS_TrnSupports
                                                       join s in db.IS_TrnActivities
                                                       on d.Id equals s.SupportId
                                                       into joinActivities
                                                       from activities in joinActivities.DefaultIfEmpty()
                                                       where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                                       && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                                       && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                       && d.SupportType == "Technical"
                                                       && d.SupportStatus == documentStatus
                                                       select new Entities.TrnActivity
                                                       {
                                                           Id = activities.Id == null ? 0 : activities.Id,
                                                           DocumentNumber = "SN - " + d.SupportNumber,
                                                           ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                           Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                           Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                           StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                           StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                           LeadId = null,
                                                           QuotationId = null,
                                                           DeliveryId = null,
                                                           SupportId = d.Id,
                                                           SoftwareDevelopmentId = null,
                                                           CustomerId = d.CustomerId,
                                                           ProductId = d.ProductId,
                                                           ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                           NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                           ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                           ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                           HeaderStatus = d.SupportStatus,
                                                           EncodedBy = d.MstUser.FullName
                                                       };

                                        return supports.ToList();
                                    }
                                    else
                                    {
                                        if (document.Equals("Support - Functional"))
                                        {
                                            var supports = from d in db.IS_TrnSupports
                                                           join s in db.IS_TrnActivities
                                                           on d.Id equals s.SupportId
                                                           into joinActivities
                                                           from activities in joinActivities.DefaultIfEmpty()
                                                           where (activities.ActivityDate != null ? activities.ActivityDate >= Convert.ToDateTime(startDate) : d.SupportDate >= Convert.ToDateTime(startDate))
                                                           && (activities.ActivityDate != null ? activities.ActivityDate <= Convert.ToDateTime(endDate) : d.SupportDate <= Convert.ToDateTime(endDate))
                                                           && (activities.ActivityDate == null ? d.SupportDate <= Convert.ToDateTime(endDate) : activities.Id == joinActivities.OrderByDescending(s => s.Id).FirstOrDefault().Id)
                                                           && d.SupportType == "Functional"
                                                           && d.SupportStatus == documentStatus
                                                           select new Entities.TrnActivity
                                                           {
                                                               Id = activities.Id == null ? 0 : activities.Id,
                                                               DocumentNumber = "SN - " + d.SupportNumber,
                                                               ActivityDate = activities.ActivityDate == null ? d.SupportDate.ToShortDateString() : activities.ActivityDate.ToShortDateString(),
                                                               Particulars = d.MstArticle.Article + " (" + d.MstArticle1.Article + ") - " + d.Remarks,
                                                               Activity = activities.Particulars == null ? " " : activities.Particulars,
                                                               StaffUserId = activities.StaffUserId == null ? 0 : activities.StaffUserId,
                                                               StaffUser = activities.MstUser.FullName == null ? " " : activities.MstUser.FullName,
                                                               LeadId = null,
                                                               QuotationId = null,
                                                               DeliveryId = null,
                                                               SupportId = d.Id,
                                                               SoftwareDevelopmentId = null,
                                                               CustomerId = d.CustomerId,
                                                               ProductId = d.ProductId,
                                                               ParticularCategory = activities.ParticularCategory == null ? " " : activities.ParticularCategory,
                                                               NumberOfHours = activities.NumberOfHours == null ? 0 : activities.NumberOfHours,
                                                               ActivityAmount = activities.ActivityAmount == null ? 0 : activities.ActivityAmount,
                                                               ActivityStatus = activities.ActivityStatus == null ? " " : activities.ActivityStatus,
                                                               HeaderStatus = d.SupportStatus,
                                                               EncodedBy = d.MstUser.FullName
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

                if (activity.SupportId != null)
                {
                    newActivity.StaffUserId = activity.StaffUserId;
                }
                else
                {
                    newActivity.StaffUserId = userId;
                }

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
                newActivity.SoftwareDevelopmentId = activity.SoftwareDevelopmentId;
                db.IS_TrnActivities.InsertOnSubmit(newActivity);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
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

                    if (activity.SupportId != null)
                    {
                        updateActivity.StaffUserId = activity.StaffUserId;
                    }

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
                    updateActivity.SoftwareDevelopmentId = activity.SoftwareDevelopmentId;
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
