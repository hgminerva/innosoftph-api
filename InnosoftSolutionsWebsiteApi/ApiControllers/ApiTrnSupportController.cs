using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    // Router prefix for web api
    [Authorize]
    [RoutePrefix("api/support")]
    public class ApiTrnSupportController : ApiController
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

        // list support
        [HttpGet, Route("list/bySupportDateRange/{startSupportDate}/{endSupportDate}/{status}/{supportType}")]
        public List<Entities.TrnSupport> listSupportBySupportDateRange(String startSupportDate, String endSupportDate, String status, String supportType)
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
                }
            }

            String documentSupportType = "Technical";
            if (supportType.Equals("Functional"))
            {
                documentSupportType = "Functional";
            }

            if (status.Equals("ALL"))
            {
                if (supportType.Equals("ALL"))
                {
                    var supports = from d in db.IS_TrnSupports.OrderByDescending(d => d.Id)
                                   where d.SupportDate >= Convert.ToDateTime(startSupportDate)
                                   && d.SupportDate <= Convert.ToDateTime(endSupportDate)
                                   select new Entities.TrnSupport
                                   {
                                       Id = d.Id,
                                       SupportNumber = d.SupportNumber,
                                       SupportDate = d.SupportDate.ToShortDateString(),
                                       ContinuityId = d.ContinuityId,
                                       ContinuityNumber = d.IS_TrnContinuity.ContinuityNumber,
                                       IssueCategory = d.IssueCategory,
                                       Issue = d.Issue,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       SupportType = d.SupportType,
                                       Severity = d.Severity,
                                       Caller = d.Caller,
                                       Remarks = d.Remarks,
                                       ScreenShotURL = d.ScreenShotURL,
                                       EncodedByUserId = d.EncodedByUserId,
                                       EncodedByUser = d.MstUser.FullName,
                                       AssignedToUserId = d.AssignedToUserId,
                                       AssignedToUser = d.MstUser1.FullName,
                                       SupportStatus = d.SupportStatus
                                   };

                    return supports.ToList();
                }
                else
                {
                    var supports = from d in db.IS_TrnSupports.OrderByDescending(d => d.Id)
                                   where d.SupportDate >= Convert.ToDateTime(startSupportDate)
                                   && d.SupportDate <= Convert.ToDateTime(endSupportDate)
                                   && d.SupportType == documentSupportType
                                   select new Entities.TrnSupport
                                   {
                                       Id = d.Id,
                                       SupportNumber = d.SupportNumber,
                                       SupportDate = d.SupportDate.ToShortDateString(),
                                       ContinuityId = d.ContinuityId,
                                       ContinuityNumber = d.IS_TrnContinuity.ContinuityNumber,
                                       IssueCategory = d.IssueCategory,
                                       Issue = d.Issue,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       SupportType = d.SupportType,
                                       Severity = d.Severity,
                                       Caller = d.Caller,
                                       Remarks = d.Remarks,
                                       ScreenShotURL = d.ScreenShotURL,
                                       EncodedByUserId = d.EncodedByUserId,
                                       EncodedByUser = d.MstUser.FullName,
                                       AssignedToUserId = d.AssignedToUserId,
                                       AssignedToUser = d.MstUser1.FullName,
                                       SupportStatus = d.SupportStatus
                                   };

                    return supports.ToList();
                }
            }
            else
            {
                if (supportType.Equals("ALL"))
                {
                    var supports = from d in db.IS_TrnSupports.OrderByDescending(d => d.Id)
                                   where d.SupportDate >= Convert.ToDateTime(startSupportDate)
                                   && d.SupportDate <= Convert.ToDateTime(endSupportDate)
                                   && d.SupportStatus == documentStatus
                                   select new Entities.TrnSupport
                                   {
                                       Id = d.Id,
                                       SupportNumber = d.SupportNumber,
                                       SupportDate = d.SupportDate.ToShortDateString(),
                                       ContinuityId = d.ContinuityId,
                                       ContinuityNumber = d.IS_TrnContinuity.ContinuityNumber,
                                       IssueCategory = d.IssueCategory,
                                       Issue = d.Issue,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       SupportType = d.SupportType,
                                       Severity = d.Severity,
                                       Caller = d.Caller,
                                       Remarks = d.Remarks,
                                       ScreenShotURL = d.ScreenShotURL,
                                       EncodedByUserId = d.EncodedByUserId,
                                       EncodedByUser = d.MstUser.FullName,
                                       AssignedToUserId = d.AssignedToUserId,
                                       AssignedToUser = d.MstUser1.FullName,
                                       SupportStatus = d.SupportStatus
                                   };

                    return supports.ToList();
                }
                else
                {
                    var supports = from d in db.IS_TrnSupports.OrderByDescending(d => d.Id)
                                   where d.SupportDate >= Convert.ToDateTime(startSupportDate)
                                   && d.SupportDate <= Convert.ToDateTime(endSupportDate)
                                   && d.SupportStatus == documentStatus
                                   && d.SupportType == documentSupportType
                                   select new Entities.TrnSupport
                                   {
                                       Id = d.Id,
                                       SupportNumber = d.SupportNumber,
                                       SupportDate = d.SupportDate.ToShortDateString(),
                                       ContinuityId = d.ContinuityId,
                                       ContinuityNumber = d.IS_TrnContinuity.ContinuityNumber,
                                       IssueCategory = d.IssueCategory,
                                       Issue = d.Issue,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       SupportType = d.SupportType,
                                       Severity = d.Severity,
                                       Caller = d.Caller,
                                       Remarks = d.Remarks,
                                       ScreenShotURL = d.ScreenShotURL,
                                       EncodedByUserId = d.EncodedByUserId,
                                       EncodedByUser = d.MstUser.FullName,
                                       AssignedToUserId = d.AssignedToUserId,
                                       AssignedToUser = d.MstUser1.FullName,
                                       SupportStatus = d.SupportStatus
                                   };

                    return supports.ToList();
                }
            }
        }

        // get support by id
        [HttpGet, Route("get/byId/{id}")]
        public Entities.TrnSupport getSupportById(String id)
        {
            var support = from d in db.IS_TrnSupports
                          where d.Id == Convert.ToInt32(id)
                          select new Entities.TrnSupport
                          {
                              Id = d.Id,
                              SupportNumber = d.SupportNumber,
                              SupportDate = d.SupportDate.ToShortDateString(),
                              ContinuityId = d.ContinuityId,
                              ContinuityNumber = d.IS_TrnContinuity.ContinuityNumber,
                              IssueCategory = d.IssueCategory,
                              Issue = d.Issue,
                              CustomerId = d.CustomerId,
                              Customer = d.MstArticle.Article,
                              ProductId = d.ProductId,
                              Product = d.MstArticle1.Article,
                              SupportType = d.SupportType,
                              Severity = d.Severity,
                              Caller = d.Caller,
                              Remarks = d.Remarks,
                              ScreenShotURL = d.ScreenShotURL,
                              EncodedByUserId = d.EncodedByUserId,
                              EncodedByUser = d.MstUser.FullName,
                              AssignedToUserId = d.AssignedToUserId,
                              AssignedToUser = d.MstUser1.FullName,
                              SupportStatus = d.SupportStatus
                          };

            return (Entities.TrnSupport)support.FirstOrDefault();
        }

        // add support
        [HttpPost, Route("post")]
        public Int32 postSupport(Entities.TrnSupport support)
        {
            try
            {
                // get last support number
                var lastSupportNumber = from d in db.IS_TrnSupports.OrderByDescending(d => d.Id) select d;
                var supportNumberValue = "0000000001";
                if (lastSupportNumber.Any())
                {
                    var supportNumber = Convert.ToInt32(lastSupportNumber.FirstOrDefault().SupportNumber) + 0000000001;
                    supportNumberValue = fillLeadingZeroes(supportNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                var continuities = from d in db.IS_TrnContinuities where d.Id == support.ContinuityId select d;
                Data.IS_TrnSupport newSupport = new Data.IS_TrnSupport();
                newSupport.SupportNumber = supportNumberValue;
                newSupport.SupportDate = Convert.ToDateTime(support.SupportDate);
                newSupport.ContinuityId = support.ContinuityId;
                newSupport.IssueCategory = support.IssueCategory;
                newSupport.Issue = support.Issue;
                newSupport.CustomerId = continuities.FirstOrDefault().CustomerId;
                newSupport.ProductId = continuities.FirstOrDefault().ProductId;
                newSupport.SupportType = support.SupportType;
                newSupport.Severity = support.Severity;
                newSupport.Caller = support.Caller;
                newSupport.Remarks = support.Remarks;
                newSupport.ScreenShotURL = support.ScreenShotURL;
                newSupport.EncodedByUserId = userId;
                newSupport.AssignedToUserId = support.AssignedToUserId;
                newSupport.SupportStatus = support.SupportStatus;
                db.IS_TrnSupports.InsertOnSubmit(newSupport);
                db.SubmitChanges();

                return newSupport.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update support
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSupport(String id, Entities.TrnSupport support)
        {
            try
            {
                var supports = from d in db.IS_TrnSupports where d.Id == Convert.ToInt32(id) select d;
                if (supports.Any())
                {
                    var continuities = from d in db.IS_TrnContinuities where d.Id == support.ContinuityId select d;
                    var updateSupport = supports.FirstOrDefault();
                    updateSupport.SupportDate = Convert.ToDateTime(support.SupportDate);
                    updateSupport.ContinuityId = support.ContinuityId;
                    updateSupport.IssueCategory = support.IssueCategory;
                    updateSupport.Issue = support.Issue;
                    updateSupport.CustomerId = continuities.FirstOrDefault().CustomerId;
                    updateSupport.ProductId = continuities.FirstOrDefault().ProductId;
                    updateSupport.SupportType = support.SupportType;
                    updateSupport.Severity = support.Severity;
                    updateSupport.Caller = support.Caller;
                    updateSupport.Remarks = support.Remarks;
                    updateSupport.ScreenShotURL = support.ScreenShotURL;
                    updateSupport.AssignedToUserId = support.AssignedToUserId;
                    updateSupport.SupportStatus = support.SupportStatus;
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

        // delete support
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSupport(String id)
        {
            try
            {
                var supports = from d in db.IS_TrnSupports where d.Id == Convert.ToInt32(id) select d;
                if (supports.Any())
                {
                    db.IS_TrnSupports.DeleteOnSubmit(supports.First());
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
