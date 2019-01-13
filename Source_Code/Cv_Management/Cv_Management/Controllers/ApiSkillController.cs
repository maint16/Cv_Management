using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.Skill;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/skill")]
    public class ApiSkillController : ApiController
    {
        #region Properties

        private readonly ISkillDomain _skillDomain;

        #endregion

        #region Contructors

        public ApiSkillController(ISkillDomain skillDomain)
        {
            _skillDomain = skillDomain;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get skills using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchSkill([FromBody] SearchSkillViewModel condition)
        {
            condition = condition ?? new SearchSkillViewModel();
            Validate(condition);

            var skills = await _skillDomain.SearchSkillsAsync(condition);
            return Ok();
        }

        /// <summary>
        ///     Create skill
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddSkill([FromBody] AddSkillViewModel model)
        {
            if (model == null)
            {
                model = new AddSkillViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skill = await _skillDomain.AddSkillAsync(model);

            return Ok(skill);
        }

        /// <summary>
        ///     Update skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> EditSkill([FromUri] int id, [FromBody] EditSkillViewModel model)
        {
            if (model == null)
            {
                model = new EditSkillViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skill = await _skillDomain.EditSkillAsync(model);

            return Ok(skill);
        }

        /// <summary>
        ///     Delete skill with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            var result = await _skillDomain.DeleteSkillAsync(id);

            return Ok(result);
        }

        #endregion
    }
}