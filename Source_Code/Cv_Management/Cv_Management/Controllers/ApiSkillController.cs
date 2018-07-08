using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.Skill;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/skill")]
    public class ApiSkillController:ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiSkillController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchSkillViewModel model)
        {
            model = model ?? new SearchSkillViewModel();
            var skills = DbSet.Skills.AsQueryable();
            if (model.Id > 0)
                skills = skills.Where(c => c.Id == model.Id);
            if (!string.IsNullOrEmpty(model.Name))
                skills = skills.Where(c => c.Name.Contains(model.Name));
            var result = skills.Select(c => new ReadingSkillViewModel()
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
        public IHttpActionResult Create([FromBody]CreateSkillViewModel model)
        {
            if (model == null)
            {
                model = new CreateSkillViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var skill = new Skill();
            skill.Name = model.Name;
            skill.CreatedTime = DateTime.Now.ToOADate();
            skill= DbSet.Skills.Add(skill);
            DbSet.SaveChanges();
            return Ok(skill);

        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody]UpdateSkillViewModel model)
        {
            if (model == null)
            {
                model = new UpdateSkillViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //get skill
            var skill = DbSet.Skills.Find(id);
            if (skill == null)
                return NotFound();
            skill.Name = model.Name;
            skill.LastModifiedTime =DateTime.Now.ToOADate();
            DbSet.SaveChanges();
            return Ok(skill);

        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var skill = DbSet.Skills.Find(id);
            if (skill == null)
                return NotFound();
            DbSet.Skills.Remove(skill);
            return Ok();

        }
    }
}