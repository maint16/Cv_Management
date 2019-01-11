using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.PersonalSkill;
using CvManagementModel.Models.Context;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/personalSkill")]
    public class ApiPersonalSkillController : ApiController
    {
        #region Properties

        public readonly CvManagementDbContext DbSet;

        #endregion

        #region Contructors

        public ApiPersonalSkillController()
        {
            DbSet = new CvManagementDbContext();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get Personal skill using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Search([FromBody] SearchPersonalSkillViewModel model)
        {
            return Ok();
        }


        /// <summary>
        ///     Create personal skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreatePersonalSkillViewModel model)
        {
            if (model == null)
            {
                model = new CreatePersonalSkillViewModel();
                Validate(model);
            }

            return Ok();
        }


        /// <summary>
        ///     Update personal skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody] UpdatePersonalSkillViewModel model)
        {
            if (model == null)
            {
                model = new UpdatePersonalSkillViewModel();
                Validate(model);
            }
            return Ok();
        }

        /// <summary>
        ///     Delete personal skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="skillCategoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] int skillId, [FromUri] int skillCategoryId)
        {
            return Ok();
        }

        #endregion
    }
}