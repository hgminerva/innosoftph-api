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
    [RoutePrefix("api/delivery")]
    public class ApiTrnDeliveryController : ApiController
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

        // list delivery
        [HttpGet, Route("list/byDeliveryDateRange/{startDeliveryDate}/{endDeliveryDate}/{status}")]
        public List<Entities.TrnDelivery> listDeliveryByDeliveryDateRange(String startDeliveryDate, String endDeliveryDate, String status)
        {
            if (status.Equals("ALL"))
            {
                var deliveries = from d in db.IS_TrnDeliveries.OrderByDescending(d => d.Id)
                                 where d.DeliveryDate >= Convert.ToDateTime(startDeliveryDate)
                                 && d.DeliveryDate <= Convert.ToDateTime(endDeliveryDate)
                                 select new Entities.TrnDelivery
                                 {
                                     Id = d.Id,
                                     DeliveryNumber = d.DeliveryNumber,
                                     DeliveryDate = d.DeliveryDate.ToShortDateString(),
                                     QuotationId = d.QuotationId,
                                     QuotationNumber = d.IS_TrnQuotation.QuotationNumber,
                                     CustomerId = d.CustomerId,
                                     Customer = d.MstArticle.Article,
                                     ProductId = d.ProductId,
                                     Product = d.MstArticle1.Article,
                                     MeetingDate = d.MeetingDate.ToShortDateString(),
                                     Remarks = d.Remarks,
                                     SalesUserId = d.SalesUserId,
                                     SalesUser = d.MstUser.FullName,
                                     TechnicalUserId = d.TechnicalUserId,
                                     TechnicalUser = d.MstUser1.FullName,
                                     FunctionalUserId = d.FunctionalUserId,
                                     FunctionalUser = d.MstUser2.FullName,
                                     DeliveryStatus = d.DeliveryStatus
                                 };

                return deliveries.ToList();
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
                    if (status.Equals("CANCELLED"))
                    {
                        documentStatus = "CANCELLED";
                    }
                    else
                    {
                        if (status.Equals("FOR CLOSING"))
                        {
                            documentStatus = "FOR CLOSING";
                        }
                        else
                        {
                            if (status.Equals("DUPLICATE"))
                            {
                                documentStatus = "DUPLICATE";
                            }
                        }
                    }
                }

                var deliveries = from d in db.IS_TrnDeliveries.OrderByDescending(d => d.Id)
                                 where d.DeliveryDate >= Convert.ToDateTime(startDeliveryDate)
                                 && d.DeliveryDate <= Convert.ToDateTime(endDeliveryDate)
                                 && d.DeliveryStatus == documentStatus
                                 select new Entities.TrnDelivery
                                 {
                                     Id = d.Id,
                                     DeliveryNumber = d.DeliveryNumber,
                                     DeliveryDate = d.DeliveryDate.ToShortDateString(),
                                     QuotationId = d.QuotationId,
                                     QuotationNumber = d.IS_TrnQuotation.QuotationNumber,
                                     CustomerId = d.CustomerId,
                                     Customer = d.MstArticle.Article,
                                     ProductId = d.ProductId,
                                     Product = d.MstArticle1.Article,
                                     MeetingDate = d.MeetingDate.ToShortDateString(),
                                     Remarks = d.Remarks,
                                     SalesUserId = d.SalesUserId,
                                     SalesUser = d.MstUser.FullName,
                                     TechnicalUserId = d.TechnicalUserId,
                                     TechnicalUser = d.MstUser1.FullName,
                                     FunctionalUserId = d.FunctionalUserId,
                                     FunctionalUser = d.MstUser2.FullName,
                                     DeliveryStatus = d.DeliveryStatus
                                 };

                return deliveries.ToList();
            }

        }

        // list delivery
        [HttpGet, Route("list/byDeliveryStatus")]
        public List<Entities.TrnDelivery> listDeliveryByDeliveryStatus()
        {
            var deliveries = from d in db.IS_TrnDeliveries.OrderBy(d => d.MstArticle.Article)
                             //where d.DeliveryStatus == "OPEN"
                             select new Entities.TrnDelivery
                             {
                                 Id = d.Id,
                                 DeliveryNumber = d.DeliveryNumber,
                                 DeliveryDate = d.DeliveryDate.ToShortDateString(),
                                 QuotationId = d.QuotationId,
                                 QuotationNumber = d.IS_TrnQuotation.QuotationNumber,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 MeetingDate = d.MeetingDate.ToShortDateString(),
                                 Remarks = d.Remarks,
                                 SalesUserId = d.SalesUserId,
                                 SalesUser = d.MstUser.FullName,
                                 TechnicalUserId = d.TechnicalUserId,
                                 TechnicalUser = d.MstUser1.FullName,
                                 FunctionalUserId = d.FunctionalUserId,
                                 FunctionalUser = d.MstUser2.FullName,
                                 DeliveryStatus = d.DeliveryStatus
                             };

            return deliveries.ToList();
        }

        // get delivery by id
        [HttpGet, Route("get/byId/{id}")]
        public Entities.TrnDelivery getDeliveryById(String id)
        {
            var delivery = from d in db.IS_TrnDeliveries
                           where d.Id == Convert.ToInt32(id)
                           select new Entities.TrnDelivery
                           {
                               Id = d.Id,
                               DeliveryNumber = d.DeliveryNumber,
                               DeliveryDate = d.DeliveryDate.ToShortDateString(),
                               QuotationId = d.QuotationId,
                               QuotationNumber = d.IS_TrnQuotation.QuotationNumber,
                               CustomerId = d.CustomerId,
                               Customer = d.MstArticle.Article,
                               CustomerContactNumber = d.MstArticle.ContactNumber,
                               CustomerAddress = d.MstArticle.Address,
                               ProductId = d.ProductId,
                               Product = d.MstArticle1.Article,
                               MeetingDate = d.MeetingDate.ToShortDateString(),
                               Remarks = d.Remarks,
                               SalesUserId = d.SalesUserId,
                               SalesUser = d.MstUser.FullName,
                               TechnicalUserId = d.TechnicalUserId,
                               TechnicalUser = d.MstUser1.FullName,
                               FunctionalUserId = d.FunctionalUserId,
                               FunctionalUser = d.MstUser2.FullName,
                               DeliveryStatus = d.DeliveryStatus
                           };

            return (Entities.TrnDelivery)delivery.FirstOrDefault();
        }

        // add delivery
        [HttpPost, Route("post")]
        public Int32 postDelivery(Entities.TrnDelivery delivery)
        {
            try
            {
                // get last delivery number
                var lastDeliveryNumber = from d in db.IS_TrnDeliveries.OrderByDescending(d => d.Id) select d;
                var deliveryNumberValue = "0000000001";
                if (lastDeliveryNumber.Any())
                {
                    var deliveryNumber = Convert.ToInt32(lastDeliveryNumber.FirstOrDefault().DeliveryNumber) + 0000000001;
                    deliveryNumberValue = fillLeadingZeroes(deliveryNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                var quotations = from d in db.IS_TrnQuotations where d.Id == delivery.QuotationId select d;
                Data.IS_TrnDelivery newDelivery = new Data.IS_TrnDelivery();
                newDelivery.DeliveryNumber = deliveryNumberValue;
                newDelivery.DeliveryDate = Convert.ToDateTime(delivery.DeliveryDate);
                newDelivery.QuotationId = delivery.QuotationId;
                newDelivery.CustomerId = quotations.FirstOrDefault().CustomerId;
                newDelivery.ProductId = quotations.FirstOrDefault().ProductId;
                newDelivery.MeetingDate = Convert.ToDateTime(delivery.MeetingDate);
                newDelivery.Remarks = delivery.Remarks;
                newDelivery.SalesUserId = userId;
                newDelivery.TechnicalUserId = delivery.TechnicalUserId;
                newDelivery.FunctionalUserId = delivery.FunctionalUserId;
                newDelivery.DeliveryStatus = delivery.DeliveryStatus;
                db.IS_TrnDeliveries.InsertOnSubmit(newDelivery);
                db.SubmitChanges();

                return newDelivery.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update delivery
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDeliver(String id, Entities.TrnDelivery delivery)
        {
            try
            {
                var deliveries = from d in db.IS_TrnDeliveries where d.Id == Convert.ToInt32(id) select d;
                if (deliveries.Any())
                {
                    var quotations = from d in db.IS_TrnQuotations where d.Id == delivery.QuotationId select d;
                    var updateDelivery = deliveries.FirstOrDefault();
                    updateDelivery.DeliveryDate = Convert.ToDateTime(delivery.DeliveryDate);
                    updateDelivery.QuotationId = delivery.QuotationId;
                    updateDelivery.CustomerId = quotations.FirstOrDefault().CustomerId;
                    updateDelivery.ProductId = quotations.FirstOrDefault().ProductId;
                    updateDelivery.MeetingDate = Convert.ToDateTime(delivery.MeetingDate);
                    updateDelivery.Remarks = delivery.Remarks;
                    updateDelivery.TechnicalUserId = delivery.TechnicalUserId;
                    updateDelivery.FunctionalUserId = delivery.FunctionalUserId;
                    updateDelivery.DeliveryStatus = delivery.DeliveryStatus;
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

        // delete delivery
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteDelivery(String id)
        {
            try
            {
                var deliveries = from d in db.IS_TrnDeliveries where d.Id == Convert.ToInt32(id) select d;
                if (deliveries.Any())
                {
                    db.IS_TrnDeliveries.DeleteOnSubmit(deliveries.First());
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
