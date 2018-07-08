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
    public class ApiSkillController : ApiController
    {
        #region Properties
        public readonly DbCvManagementContext DbSet;

        #endregion

        #region Contructors

        public ApiSkillController()
        {
            DbSet = new DbCvManagementContext();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get skills using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchSkillViewModel model)
        {
            model = model ?? new SearchSkillViewModel();
            var skills = DbSet.Skills.AsQueryable();
            if (model.Ids != null)
            {
                var ids = model.Ids.Where(x => x > 0).ToList();
                if (ids.Count > 0)
                    skills = skills.Where(x => ids.Contains(x.Id));

            }
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

        /// <summary>
        /// Create skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
            skill = DbSet.Skills.Add(skill);
            DbSet.SaveChanges();
            return Ok(skill);

        }

        /// <summary>
        /// Update skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
            skill.LastModifiedTime = DateTime.Now.ToOADate();
            DbSet.SaveChanges();
            return Ok(skill);

        }
        
        /// <summary>
        /// Delete skill with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        #endregion

    }
}