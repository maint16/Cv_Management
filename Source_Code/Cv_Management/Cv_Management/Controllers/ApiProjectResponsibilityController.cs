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
        #region properties

        public readonly DbCvManagementContext DbSet;

        #endregion

        #region Contructors

        public ApiProjectResponsibilityController()
        {
            DbSet = new DbCvManagementContext();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get project responsibility using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchProjectResponsibilityViewModel model)
        {
            model = model ?? new SearchProjectResponsibilityViewModel();
            var projectResponsibilitys = DbSet.ProjectResponsibilities.AsQueryable();
            if (model.ProjectIds != null)
            {
                var projectIds = model.ProjectIds.Where(x => x > 0).ToList();
                if (projectIds.Count > 0)
                    projectResponsibilitys = projectResponsibilitys.Where(x => projectIds.Contains(x.ProjectId));

            }
            if (model.ResponsibilityIds != null)
            {
                var responsibilityIds = model.ResponsibilityIds.Where(x => x > 0).ToList();
                if (responsibilityIds.Count > 0)
                    projectResponsibilitys = projectResponsibilitys.Where(x => responsibilityIds.Contains(x.RespinsibilityId));

            }
            var result = projectResponsibilitys.Select(c => new ReadingProjectResponsibilityViewModel()
            {
                ProjectId = c.ProjectId,
                ResponsibilityId = c.RespinsibilityId,
                CreatedTime = c.CreatedTime

            }).ToList();
            return Ok(result);

        }

        /// <summary>
        /// Create project responsibility
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete project responsibility
        /// </summary>
        /// <param name="responsibilityId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        #endregion

    }
}