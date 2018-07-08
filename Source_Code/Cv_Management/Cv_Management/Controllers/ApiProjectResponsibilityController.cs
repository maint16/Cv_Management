using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.Project;
using Cv_Management.ViewModel.ProjectResponsibility;
using Cv_Management.ViewModel.Responsibility;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/projectResponsibility")]
    public class ApiProjectResponsibilityController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiProjectResponsibilityController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchProjectResponsibilityViewModel model)
        {
            model = model ?? new SearchProjectResponsibilityViewModel();
            var projectResponsibilitys = DbSet.ProjectResponsibilities.AsQueryable();
            if (model.ProjectId > 0)
                projectResponsibilitys = projectResponsibilitys.Where(c => c.ProjectId == model.ProjectId);
            if (model.ResponsibilityId > 0)
                projectResponsibilitys = projectResponsibilitys.Where(c => c.RespinsibilityId == model.ResponsibilityId);

            var result = projectResponsibilitys.Select(c => new ReadingProjectResponsibilityViewModel()
            {
                ProjectId = c.ProjectId,
                ResponsibilityId = c.RespinsibilityId,
                CreatedTime = c.CreatedTime
               
            }).ToList();
            return Ok(result);

        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateProjectResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new CreateProjectResponsibilityViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var projectResponsibility = new ProjectResponsibility();
            projectResponsibility.ProjectId = model.ProjectId;
            projectResponsibility.RespinsibilityId = model.ResponsibilityId;
            projectResponsibility.CreatedTime = DateTime.Now.ToOADate();
            projectResponsibility = DbSet.ProjectResponsibilities.Add(projectResponsibility);
            DbSet.SaveChanges();
            return Ok(projectResponsibility);

        }
       
        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete([FromUri] int responsibilityId, [FromUri] int projectId)
        {
            var projectResponsibility = DbSet.ProjectResponsibilities.FirstOrDefault(c => c.RespinsibilityId == responsibilityId && c.ProjectId == projectId);

            if (projectResponsibility == null)
                return NotFound();
            DbSet.ProjectResponsibilities.Remove(projectResponsibility);
            return Ok();

        }
    }
}