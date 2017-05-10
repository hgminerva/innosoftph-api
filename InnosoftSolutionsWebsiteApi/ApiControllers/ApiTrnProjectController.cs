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
    [RoutePrefix("api/project")]
    public class ApiTrnProjectController : ApiController
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

        // list project
        [HttpGet, Route("list/byProjectDateRange/{startProjectDate}/{endProjectDate}/{status}")]
        public List<Entities.TrnProject> listProjectByProjectDateRange(String startProjectDate, String endProjectDate, String status)
        {
            if (status.Equals("ALL"))
            {
                var projects = from d in db.IS_TrnProjects.OrderByDescending(d => d.Id)
                               where d.ProjectDate >= Convert.ToDateTime(startProjectDate)
                               && d.ProjectDate <= Convert.ToDateTime(endProjectDate)
                               select new Entities.TrnProject
                               {
                                   Id = d.Id,
                                   ProjectNumber = d.ProjectNumber,
                                   ProjectDate = d.ProjectDate.ToShortDateString(),
                                   ProjectName = d.ProjectName,
                                   ProjectType = d.ProjectType,
                                   CustomerId = d.CustomerId,
                                   Customer = d.MstArticle.Article,
                                   Particulars = d.Particulars,
                                   EncodedByUserId = d.EncodedByUserId,
                                   EncodedByUser = d.MstUser.FullName,
                                   ProjectManagerUserId = d.ProjectManagerUserId,
                                   ProjectManagerUser = d.MstUser1.FullName,
                                   ProjectStartDate = d.ProjectStartDate.ToShortDateString(),
                                   ProjectEndDate = d.ProjectEndDate.ToShortDateString(),
                                   ProjectStatus = d.ProjectStatus
                               };

                return projects.ToList();
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

                var projects = from d in db.IS_TrnProjects.OrderByDescending(d => d.Id)
                               where d.ProjectDate >= Convert.ToDateTime(startProjectDate)
                               && d.ProjectDate <= Convert.ToDateTime(endProjectDate)
                               && d.ProjectStatus == documentStatus
                               select new Entities.TrnProject
                               {
                                   Id = d.Id,
                                   ProjectNumber = d.ProjectNumber,
                                   ProjectDate = d.ProjectDate.ToShortDateString(),
                                   ProjectName = d.ProjectName,
                                   ProjectType = d.ProjectType,
                                   CustomerId = d.CustomerId,
                                   Customer = d.MstArticle.Article,
                                   Particulars = d.Particulars,
                                   EncodedByUserId = d.EncodedByUserId,
                                   EncodedByUser = d.MstUser.FullName,
                                   ProjectManagerUserId = d.ProjectManagerUserId,
                                   ProjectManagerUser = d.MstUser1.FullName,
                                   ProjectStartDate = d.ProjectStartDate.ToShortDateString(),
                                   ProjectEndDate = d.ProjectEndDate.ToShortDateString(),
                                   ProjectStatus = d.ProjectStatus
                               };

                return projects.ToList();
            }
        }

        // list project by status
        [HttpGet, Route("list/byProjectStatus")]
        public List<Entities.TrnProject> listContinuityByProjectStatus()
        {
            var projects = from d in db.IS_TrnProjects.OrderBy(d => d.ProjectName)
                           //where d.ProjectStatus == "OPEN"
                           select new Entities.TrnProject
                           {
                               Id = d.Id,
                               ProjectNumber = d.ProjectNumber,
                               ProjectDate = d.ProjectDate.ToShortDateString(),
                               ProjectName = d.ProjectName,
                               ProjectType = d.ProjectType,
                               CustomerId = d.CustomerId,
                               Customer = d.MstArticle.Article,
                               Particulars = d.Particulars,
                               EncodedByUserId = d.EncodedByUserId,
                               EncodedByUser = d.MstUser.FullName,
                               ProjectManagerUserId = d.ProjectManagerUserId,
                               ProjectManagerUser = d.MstUser1.FullName,
                               ProjectStartDate = d.ProjectStartDate.ToShortDateString(),
                               ProjectEndDate = d.ProjectEndDate.ToShortDateString(),
                               ProjectStatus = d.ProjectStatus
                           };

            return projects.ToList();
        }

        // add project
        [HttpPost, Route("post")]
        public HttpResponseMessage postProject(Entities.TrnProject project)
        {
            try
            {
                // get last project number
                var lastProjectNumber = from d in db.IS_TrnProjects.OrderByDescending(d => d.Id) select d;
                var projectNumberValue = "0000000001";
                if (lastProjectNumber.Any())
                {
                    var projectNumber = Convert.ToInt32(lastProjectNumber.FirstOrDefault().ProjectNumber) + 0000000001;
                    projectNumberValue = fillLeadingZeroes(projectNumber, 10);
                }
                var userId = (from d in db.MstUsers where d.UserId == User.Identity.GetUserId() select d.Id).FirstOrDefault();
                Data.IS_TrnProject newProject = new Data.IS_TrnProject();
                newProject.ProjectNumber = projectNumberValue;
                newProject.ProjectDate = Convert.ToDateTime(project.ProjectDate);
                newProject.ProjectName = project.ProjectName;
                newProject.ProjectType = project.ProjectType;
                newProject.CustomerId = project.CustomerId;
                newProject.Particulars = project.Particulars;
                newProject.EncodedByUserId = userId;
                newProject.ProjectManagerUserId = project.ProjectManagerUserId;
                newProject.ProjectStartDate = Convert.ToDateTime(project.ProjectStartDate);
                newProject.ProjectEndDate = Convert.ToDateTime(project.ProjectEndDate);
                newProject.ProjectStatus = project.ProjectStatus;
                db.IS_TrnProjects.InsertOnSubmit(newProject);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // update project
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putProject(String id, Entities.TrnProject project)
        {
            try
            {
                var projects = from d in db.IS_TrnProjects where d.Id == Convert.ToInt32(id) select d;
                if (projects.Any())
                {
                    var updateProject = projects.FirstOrDefault();
                    updateProject.ProjectDate = Convert.ToDateTime(project.ProjectDate);
                    updateProject.ProjectName = project.ProjectName;
                    updateProject.ProjectType = project.ProjectType;
                    updateProject.CustomerId = project.CustomerId;
                    updateProject.Particulars = project.Particulars;
                    updateProject.ProjectManagerUserId = project.ProjectManagerUserId;
                    updateProject.ProjectStartDate = Convert.ToDateTime(project.ProjectStartDate);
                    updateProject.ProjectEndDate = Convert.ToDateTime(project.ProjectEndDate);
                    updateProject.ProjectStatus = project.ProjectStatus;
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

        // delete project
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteProject(String id)
        {
            try
            {
                var projects = from d in db.IS_TrnProjects where d.Id == Convert.ToInt32(id) select d;
                if (projects.Any())
                {
                    db.IS_TrnProjects.DeleteOnSubmit(projects.First());
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
