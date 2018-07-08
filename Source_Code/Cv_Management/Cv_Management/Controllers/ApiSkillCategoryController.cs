using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.SkillCategory;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/skillCategory")]
    public class ApiSkillCategoryController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiSkillCategoryController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchSkillCategoryViewModel model)
        {
            model = model ?? new SearchSkillCategoryViewModel();
            var skillCategorys = DbSet.SkillCategories.AsQueryable();
            if (model.Id > 0)
                skillCategorys = skillCategorys.Where(c => c.Id == model.Id);
            if (model.UserId > 0)
                skillCategorys = skillCategorys.Where(c => c.UserId == model.UserId);
            if (!string.IsNullOrEmpty(model.Name))
                skillCategorys = skillCategorys.Where(c => c.Name.Contains(model.Name));
            var result = skillCategorys.Select(c => new ReadingSkillCategoryViewModel()
            {
                Name = c.Name,
                CreatedTime = c.CreatedTime,
                Id = c.Id,
                UserId = c.UserId
            }).ToList();
            return Ok(result);

        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateSkillCategoryViewModel model)
        {
            if (model == null)
            {
                model = new CreateSkillCategoryViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var skillCategory = new SkillCategory();
            skillCategory.Name = model.Name;
            skillCategory.UserId = model.UserId;
            if(model.Photo != null)
                skillCategory.Photo= Convert.ToBase64String(model.Photo.Buffer);
            skillCategory.CreatedTime = DateTime.Now.ToOADate();
            skillCategory = DbSet.SkillCategories.Add(skillCategory);
            DbSet.SaveChanges();
            return Ok(skillCategory);

        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody]UpdateSkillCategoryViewModel model)
        {
            if (model == null)
            {
                model = new UpdateSkillCategoryViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //get SkillCategory
            var skillCategory = DbSet.SkillCategories.Find(id);
            if (skillCategory == null)
                return NotFound();
            skillCategory.Name = model.Name;
            skillCategory.UserId = model.UserId;
            if (model.Photo != null)
                skillCategory.Photo = Convert.ToBase64String(model.Photo.Buffer);
            DbSet.SaveChanges();
            return Ok(skillCategory);

        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var skillCategory = DbSet.SkillCategories.Find(id);
            if (skillCategory == null)
                return NotFound();
            DbSet.SkillCategories.Remove(skillCategory);
            return Ok();

        }
    }
}