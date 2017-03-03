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
    [RoutePrefix("api/quotation")]
    public class ApiTrnQuotationController : ApiController
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

        // list quotation
        [HttpGet, Route("list/byQuotationDateRange/{startQuotationDate}/{endQuotationDate}/{status}")]
        public List<Entities.TrnQuotation> listQuotationByLeadDateRange(String startQuotationDate, String endQuotationDate, String status)
        {
            if (status.Equals("ALL"))
            {
                var quotations = from d in db.IS_TrnQuotations.OrderByDescending(d => d.Id)
                                 where d.QuotationDate >= Convert.ToDateTime(startQuotationDate)
                                 && d.QuotationDate <= Convert.ToDateTime(endQuotationDate)
                                 select new Entities.TrnQuotation
                                 {
                                     Id = d.Id,
                                     QuotationNumber = d.QuotationNumber,
                                     QuotationDate = d.QuotationDate.ToShortDateString(),
                                     LeadId = d.LeadId,
                                     LeadNumber = d.IS_TrnLead.LeadNumber,
                                     CustomerId = d.CustomerId,
                                     Customer = d.MstArticle.Article,
                                     ProductId = d.ProductId,
                                     Product = d.MstArticle1.Article,
                                     Remarks = d.Remarks,
                                     EncodedByUserId = d.EncodedByUserId,
                                     EncodedByUser = d.MstUser.FullName,
                                     QuotationStatus = d.QuotationStatus
                                 };

                return quotations.ToList();
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
                }

                var quotations = from d in db.IS_TrnQuotations.OrderByDescending(d => d.Id)
                                 where d.QuotationDate >= Convert.ToDateTime(startQuotationDate)
                                 && d.QuotationDate <= Convert.ToDateTime(endQuotationDate)
                                 && d.QuotationStatus == documentStatus
                                 select new Entities.TrnQuotation
                                 {
                                     Id = d.Id,
                                     QuotationNumber = d.QuotationNumber,
                                     QuotationDate = d.QuotationDate.ToShortDateString(),
                                     LeadId = d.LeadId,
                                     LeadNumber = d.IS_TrnLead.LeadNumber,
                                     CustomerId = d.CustomerId,
                                     Customer = d.MstArticle.Article,
                                     ProductId = d.ProductId,
                                     Product = d.MstArticle1.Article,
                                     Remarks = d.Remarks,
                                     EncodedByUserId = d.EncodedByUserId,
                                     EncodedByUser = d.MstUser.FullName,
                                     QuotationStatus = d.QuotationStatus
                                 };

                return quotations.ToList();
            }
        }

        // list quotation
        [HttpGet, Route("list/byQuotationStatus")]
        public List<Entities.TrnQuotation> listLeadByQuotationStatus()
        {
            var quotations = from d in db.IS_TrnQuotations
                             where d.QuotationStatus == "OPEN"
                             select new Entities.TrnQuotation
                             {
                                 Id = d.Id,
                                 QuotationNumber = d.QuotationNumber,
                                 QuotationDate = d.QuotationDate.ToShortDateString(),
                                 LeadId = d.LeadId,
                                 LeadNumber = d.IS_TrnLead.LeadNumber,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstArticle.Article,
                                 ProductId = d.ProductId,
                                 Product = d.MstArticle1.Article,
                                 Remarks = d.Remarks,
                                 EncodedByUserId = d.EncodedByUserId,
                                 EncodedByUser = d.MstUser.FullName,
                                 QuotationStatus = d.QuotationStatus
                             };

            return quotations.ToList();
        }

        // get quotation by id
        [HttpGet, Route("get/byId/{id}")]
        public Entities.TrnQuotation getQuotationById(String id)
        {
            var quotation = from d in db.IS_TrnQuotations
                            where d.Id == Convert.ToInt32(id)
                            select new Entities.TrnQuotation
                            {
                                Id = d.Id,
                                QuotationNumber = d.QuotationNumber,
                                QuotationDate = d.QuotationDate.ToShortDateString(),
                                LeadId = d.LeadId,
                                LeadNumber = d.IS_TrnLead.LeadNumber,
                                CustomerId = d.CustomerId,
                                Customer = d.MstArticle.Article,
                                ProductId = d.ProductId,
                                Product = d.MstArticle1.Article,
                                Remarks = d.Remarks,
                                EncodedByUserId = d.EncodedByUserId,
                                EncodedByUser = d.MstUser.FullName,
                                QuotationStatus = d.QuotationStatus
                            };

            return (Entities.TrnQuotation)quotation.FirstOrDefault();
        }

        // add quotation
        [HttpPost, Route("post")]
        public Int32 postQuotation(Entities.TrnQuotation quotation)
        {
            try
            {
                // get last quotation number
                var lastQuotationNumber = from d in db.IS_TrnQuotations.OrderByDescending(d => d.Id) select d;
                var quotationNumberValue = "0000000001";
                if (lastQuotationNumber.Any())
                {
                    var quotationNumber = Convert.ToInt32(lastQuotationNumber.FirstOrDefault().QuotationNumber) + 0000000001;
                    quotationNumberValue = fillLeadingZeroes(quotationNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnQuotation newQuotation = new Data.IS_TrnQuotation();
                newQuotation.QuotationNumber = quotationNumberValue;
                newQuotation.QuotationDate = Convert.ToDateTime(quotation.QuotationDate);
                newQuotation.LeadId = quotation.LeadId;
                newQuotation.CustomerId = quotation.CustomerId;
                newQuotation.ProductId = quotation.ProductId;
                newQuotation.Remarks = quotation.Remarks;
                newQuotation.EncodedByUserId = userId;
                newQuotation.QuotationStatus = quotation.QuotationStatus;
                db.IS_TrnQuotations.InsertOnSubmit(newQuotation);
                db.SubmitChanges();

                return newQuotation.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update quotation
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putQuotation(String id, Entities.TrnQuotation quotation)
        {
            try
            {
                var quotations = from d in db.IS_TrnQuotations where d.Id == Convert.ToInt32(id) select d;
                if (quotations.Any())
                {
                    var updateQuotation = quotations.FirstOrDefault();
                    updateQuotation.QuotationDate = Convert.ToDateTime(quotation.QuotationDate);
                    updateQuotation.LeadId = quotation.LeadId;
                    updateQuotation.CustomerId = quotation.CustomerId;
                    updateQuotation.ProductId = quotation.ProductId;
                    updateQuotation.Remarks = quotation.Remarks;
                    //updateQuotation.EncodedByUserId = quotation.EncodedByUserId;
                    updateQuotation.QuotationStatus = quotation.QuotationStatus;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No data found from the server.");
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Something's went wrong!");
            }
        }

        // delete quotation
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteQuotation(String id)
        {
            try
            {
                var quotations = from d in db.IS_TrnQuotations where d.Id == Convert.ToInt32(id) select d;
                if (quotations.Any())
                {
                    db.IS_TrnQuotations.DeleteOnSubmit(quotations.First());
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
