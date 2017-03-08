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
    [RoutePrefix("api/softwareDevelopment")]
    public class ApiTrnSoftwareDevelopmentController : ApiController
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

        // list softwareDevelopment
        [HttpGet, Route("list/bySoftwareDevelopmentDateRange/{startSoftwareDevelopmentDate}/{endSoftwareDevelopmentDate}/{status}")]
        public List<Entities.TrnSoftwareDevelopment> listSoftwareDevelopmentBySoftwareDevelopmentDateRange(String startSoftwareDevelopmentDate, String endSoftwareDevelopmentDate, String status)
        {
            if (status.Equals("ALL"))
            {
                var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments.OrderByDescending(d => d.Id)
                                           where d.SoftDevDate >= Convert.ToDateTime(startSoftwareDevelopmentDate)
                                           && d.SoftDevDate <= Convert.ToDateTime(endSoftwareDevelopmentDate)
                                           select new Entities.TrnSoftwareDevelopment
                                           {
                                               Id = d.Id,
                                               SoftDevNumber = d.SoftDevNumber,
                                               SoftDevDate = d.SoftDevDate.ToShortDateString(),
                                               ProjectId = d.ProjectId,
                                               ProjectNumber = d.IS_TrnProject.ProjectNumber,
                                               ProjectName = d.IS_TrnProject.ProjectName,
                                               Task = d.Task,
                                               Remarks = d.Remarks,
                                               NumberOfHours = d.NumberOfHours,
                                               EncodedByUserId = d.EncodedByUserId,
                                               EncodedByUser = d.MstUser.FullName,
                                               AssignedToUserId = d.AssignedToUserId,
                                               AssignedToUser = d.MstUser1.FullName,
                                               SoftDevStatus = d.SoftDevStatus
                                           };

                return softwareDevelopments.ToList();
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

                var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments.OrderByDescending(d => d.Id)
                                           where d.SoftDevDate >= Convert.ToDateTime(startSoftwareDevelopmentDate)
                                           && d.SoftDevDate <= Convert.ToDateTime(endSoftwareDevelopmentDate)
                                           && d.SoftDevStatus == documentStatus
                                           select new Entities.TrnSoftwareDevelopment
                                           {
                                               Id = d.Id,
                                               SoftDevNumber = d.SoftDevNumber,
                                               SoftDevDate = d.SoftDevDate.ToShortDateString(),
                                               ProjectId = d.ProjectId,
                                               ProjectNumber = d.IS_TrnProject.ProjectNumber,
                                               ProjectName = d.IS_TrnProject.ProjectName,
                                               Task = d.Task,
                                               Remarks = d.Remarks,
                                               NumberOfHours = d.NumberOfHours,
                                               EncodedByUserId = d.EncodedByUserId,
                                               EncodedByUser = d.MstUser.FullName,
                                               AssignedToUserId = d.AssignedToUserId,
                                               AssignedToUser = d.MstUser1.FullName,
                                               SoftDevStatus = d.SoftDevStatus
                                           };

                return softwareDevelopments.ToList();
            }
        }

        // list softwareDevelopment
        [HttpGet, Route("list/bySoftwareDevelopmentStatus")]
        public List<Entities.TrnSoftwareDevelopment> listSoftwareDevelopmentBySoftwareDevelopmentStatus()
        {
            var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments.OrderBy(d => d.IS_TrnProject.ProjectName)
                                       where d.SoftDevStatus == "OPEN"
                                       select new Entities.TrnSoftwareDevelopment
                                       {
                                           Id = d.Id,
                                           SoftDevNumber = d.SoftDevNumber,
                                           SoftDevDate = d.SoftDevDate.ToShortDateString(),
                                           ProjectId = d.ProjectId,
                                           ProjectNumber = d.IS_TrnProject.ProjectNumber,
                                           ProjectName = d.IS_TrnProject.ProjectName,
                                           Task = d.Task,
                                           Remarks = d.Remarks,
                                           NumberOfHours = d.NumberOfHours,
                                           EncodedByUserId = d.EncodedByUserId,
                                           EncodedByUser = d.MstUser.FullName,
                                           AssignedToUserId = d.AssignedToUserId,
                                           AssignedToUser = d.MstUser1.FullName,
                                           SoftDevStatus = d.SoftDevStatus
                                       };

            return softwareDevelopments.ToList();
        }

        // get softwareDevelopment by id
        [HttpGet, Route("get/byId/{id}")]
        public Entities.TrnSoftwareDevelopment getSoftwareDevelopmentById(String id)
        {
            var softwareDevelopment = from d in db.IS_TrnSoftwareDevelopments
                                      where d.Id == Convert.ToInt32(id)
                                      select new Entities.TrnSoftwareDevelopment
                                      {
                                          Id = d.Id,
                                          SoftDevNumber = d.SoftDevNumber,
                                          SoftDevDate = d.SoftDevDate.ToShortDateString(),
                                          ProjectId = d.ProjectId,
                                          ProjectNumber = d.IS_TrnProject.ProjectNumber,
                                          ProjectName = d.IS_TrnProject.ProjectName,
                                          Task = d.Task,
                                          Remarks = d.Remarks,
                                          NumberOfHours = d.NumberOfHours,
                                          EncodedByUserId = d.EncodedByUserId,
                                          EncodedByUser = d.MstUser.FullName,
                                          AssignedToUserId = d.AssignedToUserId,
                                          AssignedToUser = d.MstUser1.FullName,
                                          SoftDevStatus = d.SoftDevStatus
                                      };

            return (Entities.TrnSoftwareDevelopment)softwareDevelopment.FirstOrDefault();
        }

        // add softwareDevelopment
        [HttpPost, Route("post")]
        public Int32 postSoftwareDevelopment(Entities.TrnSoftwareDevelopment softwareDevelopment)
        {
            try
            {
                // get last softwareDevelopment number
                var lastSoftwareDevelopmentNumber = from d in db.IS_TrnSoftwareDevelopments.OrderByDescending(d => d.Id) select d;
                var softwareDevelopmentNumberValue = "0000000001";
                if (lastSoftwareDevelopmentNumber.Any())
                {
                    var softwareDevelopmentNumber = Convert.ToInt32(lastSoftwareDevelopmentNumber.FirstOrDefault().SoftDevNumber) + 0000000001;
                    softwareDevelopmentNumberValue = fillLeadingZeroes(softwareDevelopmentNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnSoftwareDevelopment newSoftwareDevelopment = new Data.IS_TrnSoftwareDevelopment();
                newSoftwareDevelopment.SoftDevNumber = softwareDevelopmentNumberValue;
                newSoftwareDevelopment.SoftDevDate = Convert.ToDateTime(softwareDevelopment.SoftDevDate);
                newSoftwareDevelopment.ProjectId = softwareDevelopment.ProjectId;
                newSoftwareDevelopment.Task = softwareDevelopment.Task;
                newSoftwareDevelopment.Remarks = softwareDevelopment.Remarks;
                newSoftwareDevelopment.NumberOfHours = softwareDevelopment.NumberOfHours;
                newSoftwareDevelopment.EncodedByUserId = userId;
                newSoftwareDevelopment.AssignedToUserId = softwareDevelopment.AssignedToUserId;
                newSoftwareDevelopment.SoftDevStatus = softwareDevelopment.SoftDevStatus;
                db.IS_TrnSoftwareDevelopments.InsertOnSubmit(newSoftwareDevelopment);
                db.SubmitChanges();

                return newSoftwareDevelopment.Id;
            }
            catch
            {
                return 0;
            }
        }

        // update softwareDevelopment
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putDeliver(String id, Entities.TrnSoftwareDevelopment softwareDevelopment)
        {
            try
            {
                var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments where d.Id == Convert.ToInt32(id) select d;
                if (softwareDevelopments.Any())
                {
                    var updateSoftwareDevelopment = softwareDevelopments.FirstOrDefault();
                    updateSoftwareDevelopment.SoftDevDate = Convert.ToDateTime(softwareDevelopment.SoftDevDate);
                    updateSoftwareDevelopment.ProjectId = softwareDevelopment.ProjectId;
                    updateSoftwareDevelopment.Task = softwareDevelopment.Task;
                    updateSoftwareDevelopment.Remarks = softwareDevelopment.Remarks;
                    updateSoftwareDevelopment.NumberOfHours = softwareDevelopment.NumberOfHours;
                    updateSoftwareDevelopment.AssignedToUserId = softwareDevelopment.AssignedToUserId;
                    updateSoftwareDevelopment.SoftDevStatus = softwareDevelopment.SoftDevStatus;
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

        // delete softwareDevelopment
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSoftwareDevelopment(String id)
        {
            try
            {
                var softwareDevelopments = from d in db.IS_TrnSoftwareDevelopments where d.Id == Convert.ToInt32(id) select d;
                if (softwareDevelopments.Any())
                {
                    db.IS_TrnSoftwareDevelopments.DeleteOnSubmit(softwareDevelopments.First());
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
