using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.Responsibility;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/responsibility")]
    public class ApiResponsibilityController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiResponsibilityController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchResponsibilityViewModel model)
        {
            model = model ?? new SearchResponsibilityViewModel();
            var responsibilitys = DbSet.Responsibilities.AsQueryable();
            if (model.Id > 0)
                responsibilitys = responsibilitys.Where(c => c.Id == model.Id);
            if (!string.IsNullOrEmpty(model.Name))
                responsibilitys = responsibilitys.Where(c => c.Name.Contains(model.Name));
            var result = responsibilitys.Select(c => new ReadingResponsibilityViewModel()
            {
                Name = c.Name,
                CreatedTime = c.CreatedTime,
                Id = c.Id,
                LastModifiedTime = c.LastModifiedTime
            }).ToList();
            return Ok(result);

        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new CreateResponsibilityViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var responsibility = new Responsibility();
            responsibility.Name = model.Name;
            responsibility.CreatedTime = DateTime.Now.ToOADate();
            responsibility = DbSet.Responsibilities.Add(responsibility);
            DbSet.SaveChanges();
            return Ok(responsibility);

        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody]UpdateResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new UpdateResponsibilityViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //get Responsibility
            var responsibility = DbSet.Responsibilities.Find(id);
            if (responsibility == null)
                return NotFound();
            responsibility.Name = model.Name;
            responsibility.LastModifiedTime = DateTime.Now.ToOADate();
            DbSet.SaveChanges();
            return Ok(responsibility);

        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var responsibility = DbSet.Responsibilities.Find(id);
            if (responsibility == null)
                return NotFound();
            DbSet.Responsibilities.Remove(responsibility);
            return Ok();

        }
    }
}