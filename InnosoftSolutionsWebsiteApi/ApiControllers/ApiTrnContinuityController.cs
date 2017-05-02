using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace InnosoftSolutionsWebsiteApi.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/continuity")]
    public class ApiTrnContinuityController : ApiController
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

        // list continuity
        [HttpGet, Route("list/byContinuityDateRange/{startContinuityDate}/{endContinuityDate}/{status}")]
        public List<Entities.TrnContinuity> listContinuityByContinuityDateRange(String startContinuityDate, String endContinuityDate, String status)
        {
            if (status.Equals("ALL"))
            {
                var continuities = from d in db.IS_TrnContinuities.OrderByDescending(d => d.Id)
                                   where d.ExpiryDate >= Convert.ToDateTime(startContinuityDate)
                                   && d.ExpiryDate <= Convert.ToDateTime(endContinuityDate)
                                   select new Entities.TrnContinuity
                                   {
                                       Id = d.Id,
                                       ContinuityNumber = d.ContinuityNumber,
                                       ContinuityDate = d.ContinuityDate.ToShortDateString(),
                                       DeliveryId = d.DeliveryId,
                                       DeliveryNumber = d.IS_TrnDelivery.DeliveryNumber,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       ExpiryDate = d.ExpiryDate.ToShortDateString(),
                                       Remarks = d.Remarks,
                                       StaffUserId = d.StaffUserId,
                                       StaffUser = d.MstUser.FullName,
                                       ContinuityStatus = d.ContinuityStatus
                                   };

                return continuities.ToList();
            }
            else
            {
                String documentStatus = "OPEN";
                if (status.Equals("EXPIRED"))
                {
                    documentStatus = "EXPIRED";
                }

                var continuities = from d in db.IS_TrnContinuities.OrderByDescending(d => d.Id)
                                   where d.ExpiryDate >= Convert.ToDateTime(startContinuityDate)
                                   && d.ExpiryDate <= Convert.ToDateTime(endContinuityDate)
                                   && d.ContinuityStatus == documentStatus
                                   select new Entities.TrnContinuity
                                   {
                                       Id = d.Id,
                                       ContinuityNumber = d.ContinuityNumber,
                                       ContinuityDate = d.ContinuityDate.ToShortDateString(),
                                       DeliveryId = d.DeliveryId,
                                       DeliveryNumber = d.IS_TrnDelivery.DeliveryNumber,
                                       CustomerId = d.CustomerId,
                                       Customer = d.MstArticle.Article,
                                       ProductId = d.ProductId,
                                       Product = d.MstArticle1.Article,
                                       ExpiryDate = d.ExpiryDate.ToShortDateString(),
                                       Remarks = d.Remarks,
                                       StaffUserId = d.StaffUserId,
                                       StaffUser = d.MstUser.FullName,
                                       ContinuityStatus = d.ContinuityStatus
                                   };

                return continuities.ToList();
            }
        }

        // list continuity
        [HttpGet, Route("list/byContinuityStatus")]
        public List<Entities.TrnContinuity> listContinuityByContinuityStatus()
        {
            var continuities = from d in db.IS_TrnContinuities.OrderBy(d => d.MstArticle.Article)
                               //where d.ContinuityStatus == "OPEN"
                               select new Entities.TrnContinuity
                               {
                                   Id = d.Id,
                                   ContinuityNumber = d.ContinuityNumber,
                                   ContinuityDate = d.ContinuityDate.ToShortDateString(),
                                   DeliveryId = d.DeliveryId,
                                   DeliveryNumber = d.IS_TrnDelivery.DeliveryNumber,
                                   CustomerId = d.CustomerId,
                                   Customer = d.MstArticle.Article,
                                   ProductId = d.ProductId,
                                   Product = d.MstArticle1.Article,
                                   ExpiryDate = d.ExpiryDate.ToShortDateString(),
                                   Remarks = d.Remarks,
                                   StaffUserId = d.StaffUserId,
                                   StaffUser = d.MstUser.FullName,
                                   ContinuityStatus = d.ContinuityStatus
                               };

            return continuities.ToList();
        }

        // list continuity
        [HttpGet, Route("list/byCustomerId/byContinuityStatus/{customerId}")]
        public List<Entities.TrnContinuity> listContinuityByCustomerIdByContinuityStatus(String customerId)
        {
            var continuities = from d in db.IS_TrnContinuities.OrderBy(d => d.MstArticle.Article)
                               where d.CustomerId == Convert.ToInt32(customerId)
                               //&& d.ContinuityStatus == "OPEN"
                               select new Entities.TrnContinuity
                               {
                                   Id = d.Id,
                                   ContinuityNumber = d.ContinuityNumber,
                                   ContinuityDate = d.ContinuityDate.ToShortDateString(),
                                   DeliveryId = d.DeliveryId,
                                   DeliveryNumber = d.IS_TrnDelivery.DeliveryNumber,
                                   CustomerId = d.CustomerId,
                                   Customer = d.MstArticle.Article,
                                   ProductId = d.ProductId,
                                   Product = d.MstArticle1.Article,
                                   ExpiryDate = d.ExpiryDate.ToShortDateString(),
                                   Remarks = d.Remarks,
                                   StaffUserId = d.StaffUserId,
                                   StaffUser = d.MstUser.FullName,
                                   ContinuityStatus = d.ContinuityStatus
                               };

            return continuities.ToList();
        }

        // list continuity customers
        [HttpGet, Route("list/continuity/customers")]
        public List<Entities.TrnContinuity> listContinuityCustomers()
        {
            var continuities = from d in db.IS_TrnContinuities.OrderBy(d => d.MstArticle.Article)
                               group d by new
                               {
                                   CustomerId = d.CustomerId,
                                   Customer = d.MstArticle.Article
                               }
                                   into g
                                   select new Entities.TrnContinuity
                                   {
                                       CustomerId = g.Key.CustomerId,
                                       Customer = g.Key.Customer
                                   };

            return continuities.ToList();
        }

        // add continuity
        [HttpPost, Route("post")]
        public HttpResponseMessage postContinuity(Entities.TrnContinuity continuity)
        {
            try
            {
                // get last continuity number
                var lastContinuityNumber = from d in db.IS_TrnContinuities.OrderByDescending(d => d.Id) select d;
                var continuityNumberValue = "0000000001";
                if (lastContinuityNumber.Any())
                {
                    var continuityNumber = Convert.ToInt32(lastContinuityNumber.FirstOrDefault().ContinuityNumber) + 0000000001;
                    continuityNumberValue = fillLeadingZeroes(continuityNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                var deliveries = from d in db.IS_TrnDeliveries where d.Id == continuity.DeliveryId select d;
                Data.IS_TrnContinuity newContinuity = new Data.IS_TrnContinuity();
                newContinuity.ContinuityNumber = continuityNumberValue;
                newContinuity.ContinuityDate = Convert.ToDateTime(continuity.ContinuityDate);
                newContinuity.DeliveryId = continuity.DeliveryId;
                newContinuity.CustomerId = deliveries.FirstOrDefault().CustomerId;
                newContinuity.ProductId = deliveries.FirstOrDefault().ProductId;
                newContinuity.ExpiryDate = Convert.ToDateTime(continuity.ExpiryDate);
                newContinuity.Remarks = continuity.Remarks;
                newContinuity.StaffUserId = userId;
                newContinuity.ContinuityStatus = continuity.ContinuityStatus;
                db.IS_TrnContinuities.InsertOnSubmit(newContinuity);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update continuity
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putContinuity(String id, Entities.TrnContinuity continuity)
        {
            try
            {
                var continuities = from d in db.IS_TrnContinuities where d.Id == Convert.ToInt32(id) select d;
                if (continuities.Any())
                {
                    var deliveries = from d in db.IS_TrnDeliveries where d.Id == continuity.DeliveryId select d;
                    var updateContinuity = continuities.FirstOrDefault();
                    updateContinuity.ContinuityDate = Convert.ToDateTime(continuity.ContinuityDate);
                    updateContinuity.DeliveryId = continuity.DeliveryId;
                    updateContinuity.CustomerId = deliveries.FirstOrDefault().CustomerId;
                    updateContinuity.ProductId = deliveries.FirstOrDefault().ProductId;
                    updateContinuity.ExpiryDate = Convert.ToDateTime(continuity.ExpiryDate);
                    updateContinuity.Remarks = continuity.Remarks;
                    updateContinuity.ContinuityStatus = continuity.ContinuityStatus;
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

        // delete continuity
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteContinuity(String id)
        {
            try
            {
                var continuities = from d in db.IS_TrnContinuities where d.Id == Convert.ToInt32(id) select d;
                if (continuities.Any())
                {
                    db.IS_TrnContinuities.DeleteOnSubmit(continuities.First());
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
