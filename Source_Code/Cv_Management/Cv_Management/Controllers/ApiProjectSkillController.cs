using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.Project;
using Cv_Management.ViewModel.ProjectSkill;
using Cv_Management.ViewModel.Skill;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/projectSkill")]
    public class ApiProjectSkillController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiProjectSkillController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchProjectSkillViewModel model)
        {
            model = model ?? new SearchProjectSkillViewModel();
            var projectSkills = DbSet.ProjectSkills.AsQueryable();
            if (model.ProjectId > 0)
                projectSkills = projectSkills.Where(c => c.ProjectId == model.ProjectId);
            if (model.SkillId > 0)
                projectSkills = projectSkills.Where(c => c.SkillId == model.SkillId);

            var result = projectSkills.Select(c => new ReadingProjectSkillViewModel()
            {
                ProjectId = c.ProjectId,
                project = new ReadingProjectViewModel()
                {
                    Id=c.Project.Id,
                    Name = c.Project.Name,
                    UserId = c.Project.UserId,
                    Description = c.Project.Description,
                    FinishedTime = c.Project.FinishedTime,
                    StatedTime = c.Project.StatedTime
                  
                },
                SkillId = c.SkillId,
                skill = new ReadingSkillViewModel()
                {
                    Id=c.Skill.Id,
                    Name = c.Skill.Name,
                    CreatedTime = c.Skill.CreatedTime,
                    LastModifiedTime = c.Skill.LastModifiedTime
                }
            }).ToList();
            return Ok(result);

        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateProjectSkillViewModel model)
        {
            if (model == null)
            {
                model = new CreateProjectSkillViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var projectSkill = new ProjectSkill();
            projectSkill.ProjectId = model.ProjectId;
            projectSkill.SkillId = model.SkillId;
            projectSkill = DbSet.ProjectSkills.Add(projectSkill);
            DbSet.SaveChanges();
            return Ok(projectSkill);

        }
       
        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete([FromUri] int skillId, [FromUri] int projectId)
        {
            var projectSkill = DbSet.ProjectSkills.FirstOrDefault(c => c.SkillId == skillId && c.ProjectId == projectId);

            if (projectSkill == null)
                return NotFound();
            DbSet.ProjectSkills.Remove(projectSkill);
            return Ok();

        }
    }
}