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
    [RoutePrefix("api/request")]
    public class ApiTrnRequestController : ApiController
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

        // list request
        [HttpGet, Route("list/byRequestDateRange/{startRequestDate}/{endRequestDate}/{requestType}/{requestStatus}")]
        public List<Entities.TrnRequest> listRequestByRequestDateRange(String startRequestDate, String endRequestDate, String requestType, String requestStatus)
        {
            if (requestStatus.Equals("ALL"))
            {
                var requests = from d in db.IS_TrnRequests.OrderByDescending(d => d.Id)
                               where d.RequestDate >= Convert.ToDateTime(startRequestDate)
                               && d.RequestDate <= Convert.ToDateTime(endRequestDate)
                               && d.RequestType == requestType
                               select new Entities.TrnRequest
                               {
                                   Id = d.Id,
                                   RequestNumber = d.RequestNumber,
                                   RequestDate = d.RequestDate.ToShortDateString(),
                                   RequestType = d.RequestType,
                                   Particulars = d.Particulars,
                                   EncodedByUserId = d.EncodedByUserId,
                                   EncodedByUser = d.MstUser.FullName,
                                   CheckedByUserId = d.CheckedByUserId,
                                   CheckedByUser = d.MstUser1.FullName,
                                   CheckedRemarks = d.CheckedRemarks,
                                   ApprovedByUserId = d.ApprovedByUserId,
                                   ApprovedByUser = d.MstUser2.FullName,
                                   ApprovedRemarks = d.ApprovedRemarks,
                                   RequestStatus = d.RequestStatus
                               };

                return requests.ToList();
            }
            else
            {
                String documentStatus = "OPEN";
                if (requestStatus.Equals("CLOSE"))
                {
                    documentStatus = "CLOSE";
                }
                else
                {
                    if (requestStatus.Equals("CANCELLED"))
                    {
                        documentStatus = "CANCELLED";
                    }
                    else
                    {
                        if (requestStatus.Equals("DUPLICATE"))
                        {
                            documentStatus = "DUPLICATE";
                        }
                    }
                }

                var requests = from d in db.IS_TrnRequests.OrderByDescending(d => d.Id)
                               where d.RequestDate >= Convert.ToDateTime(startRequestDate)
                               && d.RequestDate <= Convert.ToDateTime(endRequestDate)
                               && d.RequestType == requestType
                               && d.RequestStatus == documentStatus
                               select new Entities.TrnRequest
                               {
                                   Id = d.Id,
                                   RequestNumber = d.RequestNumber,
                                   RequestDate = d.RequestDate.ToShortDateString(),
                                   RequestType = d.RequestType,
                                   Particulars = d.Particulars,
                                   EncodedByUserId = d.EncodedByUserId,
                                   EncodedByUser = d.MstUser.FullName,
                                   CheckedByUserId = d.CheckedByUserId,
                                   CheckedByUser = d.MstUser1.FullName,
                                   CheckedRemarks = d.CheckedRemarks,
                                   ApprovedByUserId = d.ApprovedByUserId,
                                   ApprovedByUser = d.MstUser2.FullName,
                                   ApprovedRemarks = d.ApprovedRemarks,
                                   RequestStatus = d.RequestStatus
                               };

                return requests.ToList();
            }
        }

        // add request
        [HttpPost, Route("post")]
        public HttpResponseMessage postRequest(Entities.TrnRequest request)
        {
            try
            {
                // get last request number
                var lastRequestNumber = from d in db.IS_TrnRequests.OrderByDescending(d => d.Id) select d;
                var requestNumberValue = "0000000001";
                if (lastRequestNumber.Any())
                {
                    var requestNumber = Convert.ToInt32(lastRequestNumber.FirstOrDefault().RequestNumber) + 0000000001;
                    requestNumberValue = fillLeadingZeroes(requestNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnRequest newRequest = new Data.IS_TrnRequest();
                newRequest.RequestNumber = requestNumberValue;
                newRequest.RequestDate = Convert.ToDateTime(request.RequestDate);
                newRequest.RequestType = request.RequestType;
                newRequest.Particulars = request.Particulars;
                newRequest.EncodedByUserId = userId;
                newRequest.CheckedByUserId = null;
                newRequest.CheckedRemarks = null;
                newRequest.ApprovedByUserId = null;
                newRequest.ApprovedRemarks = null;
                newRequest.RequestStatus = request.RequestStatus;
                db.IS_TrnRequests.InsertOnSubmit(newRequest);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update request
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putRequest(String id, Entities.TrnRequest request)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                if (requests.Any())
                {
                    if (requests.FirstOrDefault().EncodedByUserId == userId)
                    {
                        var updateRequest = requests.FirstOrDefault();
                        updateRequest.RequestDate = Convert.ToDateTime(request.RequestDate);
                        updateRequest.RequestType = request.RequestType;
                        updateRequest.Particulars = request.Particulars;
                        updateRequest.RequestStatus = request.RequestStatus;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
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

        // delete request
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteRequest(String id)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                if (requests.Any())
                {
                    if (requests.FirstOrDefault().EncodedByUserId == userId)
                    {
                        db.IS_TrnRequests.DeleteOnSubmit(requests.First());
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
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

        // check request
        [HttpPut, Route("check/{id}")]
        public HttpResponseMessage checkRequest(String id, Entities.TrnRequest request)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                if (requests.Any())
                {
                    var updateCheckRequest = requests.FirstOrDefault();
                    var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                    updateCheckRequest.CheckedByUserId = userId;
                    updateCheckRequest.CheckedRemarks = request.CheckedRemarks;
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

        // approve request
        [HttpPut, Route("approve/{id}")]
        public HttpResponseMessage approveRequest(String id, Entities.TrnRequest request)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                if (requests.Any())
                {
                    var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                    var updateApproveRequest = requests.FirstOrDefault();
                    updateApproveRequest.ApprovedByUserId = userId;
                    updateApproveRequest.ApprovedRemarks = request.ApprovedRemarks;
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

        // uncheck request
        [HttpPut, Route("uncheck/{id}")]
        public HttpResponseMessage uncheckRequest(String id)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                if (requests.Any())
                {
                    var updateCheckRequest = requests.FirstOrDefault();
                    var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                    updateCheckRequest.CheckedByUserId = null;
                    updateCheckRequest.CheckedRemarks = null;
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

        // disapprove request
        [HttpPut, Route("disapprove/{id}")]
        public HttpResponseMessage disapproveRequest(String id)
        {
            try
            {
                var requests = from d in db.IS_TrnRequests where d.Id == Convert.ToInt32(id) select d;
                if (requests.Any())
                {
                    var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                    var updateApproveRequest = requests.FirstOrDefault();
                    updateApproveRequest.ApprovedByUserId = null;
                    updateApproveRequest.ApprovedRemarks = null;
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
