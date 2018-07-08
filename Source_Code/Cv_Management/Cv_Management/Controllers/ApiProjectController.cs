using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.Project;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/projet")]
    public class ApiProjectController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiProjectController()
        {
            DbSet = new DbCvManagementContext();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchProjectViewModel model)
        {
            model = model ?? new SearchProjectViewModel();
            var Projects = DbSet.Projects.AsQueryable();
            if (model.Id > 0)
                Projects = Projects.Where(c => c.Id == model.Id);
            if (!string.IsNullOrEmpty(model.Name))
                Projects = Projects.Where(c => c.Name.Contains(model.Name));

            var result = Projects.Select(c => new ReadingProjectViewModel()
            {
                Name = c.Name,
                UserId = c.UserId,
                Id = c.Id,
                StatedTime = c.StatedTime,
                FinishedTime = c.FinishedTime,
                Description = c.Description
            }).ToList();
            return Ok(result);

        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateProjectViewModel model)
        {
            if (model == null)
            {
                model = new CreateProjectViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var project = new Project();
            project.UserId = model.UserId;
            project.Name = model.Name;
            project.Description = model.Description;
            project.FinishedTime = model.FinishedTime;
            project.StatedTime = model.StatedTime;
            project = DbSet.Projects.Add(project);
            DbSet.SaveChanges();
            return Ok(project);

        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody]UpdateProjectViewModel model)
        {
            if (model == null)
            {
                model = new UpdateProjectViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //get Project
            var project = DbSet.Projects.Find(id);
            if (project == null)
                return NotFound();
            project.UserId = model.UserId;
            project.Name = model.Name;
            project.Description = model.Description;
            project.FinishedTime = model.FinishedTime;
            project.StatedTime = model.StatedTime;
            DbSet.SaveChanges();
            return Ok(project);

        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var project = DbSet.Projects.Find(id);
            if (project == null)
                return NotFound();
            DbSet.Projects.Remove(project);
            return Ok();

        }
    }
}