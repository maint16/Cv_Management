﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.PersonalSkill;
using Cv_Management.ViewModel.Skill;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/personalSkill")]
    public class ApiPersonalSkillController : ApiController
    {
        #region Properties

        public readonly DbCvManagementContext DbSet;

        #endregion

        #region Contructors

        public ApiPersonalSkillController()
        {
            DbSet = new DbCvManagementContext();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Personal skill using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchPersonalSkillViewModel model)
        {
            model = model ?? new SearchPersonalSkillViewModel();
            var personalSkills = DbSet.PersonalSkills.AsQueryable();


            if (model.SkillCategoryIds != null)
            {
                var skillCategoryIds = model.SkillCategoryIds.Where(x => x > 0).ToList();
                if (skillCategoryIds.Count > 0)
                    personalSkills = personalSkills.Where(x => skillCategoryIds.Contains(x.SkillCategoryId));
            }

            if (model.SkillIds != null)
            {
                var skillIds = model.SkillIds.Where(x => x > 0).ToList();
                if (skillIds.Count > 0)
                    personalSkills = personalSkills.Where(x => skillIds.Contains(x.SkillId));
            }
            if (model.Point > 0)
                personalSkills = personalSkills.Where(c => c.Point == model.Point);

            var result = personalSkills.Select(c => new ReadingPersonalSkillViewModel()
            {
                SkillCategoryId = c.SkillCategoryId,
                SkillId = c.SkillId,
                Point = c.Point,
                CreatedTime = c.CreatedTime
            }).ToList();
            return Ok(result);

        }


        /// <summary>
        /// Create personal skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreatePersonalSkillViewModel model)
        {
            if (model == null)
            {
                model = new CreatePersonalSkillViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var personalSkill = new PersonalSkill();
            personalSkill.SkillCategoryId = model.SkillCategoryId;
            personalSkill.SkillId = model.SkillId;
            personalSkill.Point = model.Point;
            personalSkill.CreatedTime = DateTime.Now.ToOADate();
            personalSkill = DbSet.PersonalSkills.Add(personalSkill);
            DbSet.SaveChanges();
            return Ok(personalSkill);

        }


        /// <summary>
        /// Update personal skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]UpdatePersonalSkillViewModel model)
        {
            if (model == null)
            {
                model = new UpdatePersonalSkillViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var personalSkill = DbSet.PersonalSkills.FirstOrDefault(c => c.SkillCategoryId == model.SkillCategoryId && c.SkillId == model.SkillId);
            if (personalSkill == null)
                return NotFound();
            personalSkill.Point = model.Point;
            DbSet.SaveChanges();
            return Ok(personalSkill);

        }

        /// <summary>
        /// Delete personal skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="skillCategoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete([FromUri] int skillId, [FromUri] int skillCategoryId)
        {
            var personalSkill = DbSet.PersonalSkills.FirstOrDefault(c => c.SkillId == skillId && c.SkillCategoryId == skillCategoryId);

            if (personalSkill == null)
                return NotFound();
            DbSet.PersonalSkills.Remove(personalSkill);
            return Ok();

        }

        #endregion

    }
}