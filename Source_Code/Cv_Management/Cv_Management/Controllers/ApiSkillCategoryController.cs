using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.SkillCategory;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/skillCategory")]
    public class ApiSkillCategoryController : ApiController
    {
        #region Properties

        private readonly ISkilCategoryDomain _skillSkilCategoryDomain;

        #endregion

        #region Contructors

        public ApiSkillCategoryController(ISkilCategoryDomain skillCategoryDomain)
        {
            _skillSkilCategoryDomain = skillCategoryDomain;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get Skill category using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Search([FromBody] SearchSkillCategoryViewModel model)
        {
            if (model == null)
            {
                model = new SearchSkillCategoryViewModel();
                Validate(model);
            }

            var loadSkillCategoriesResult = await _skillSkilCategoryDomain.SearchSkillCategoriesAsync(model);
            return Ok(loadSkillCategoriesResult);
        }

        /// <summary>
        ///     Create Skill category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateSkillCategoryViewModel model)
        {
            if (model == null)
            {
                model = new CreateSkillCategoryViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skillCategory = await _skillSkilCategoryDomain.AddSkillCategoryAsync(model);

            return Ok(skillCategory);
        }

        /// <summary>
        ///     Update skill category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] UpdateSkillCategoryViewModel model)
        {
            if (model == null)
            {
                model = new UpdateSkillCategoryViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.Id = id;
            var skillCategory = await _skillSkilCategoryDomain.EditCategoryAsync(model);

            if (skillCategory == null)
                return NotFound();

            return Ok(skillCategory);
        }

        /// <summary>
        ///     Delete skill category with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            var result = await _skillSkilCategoryDomain.DeleteAsync(id);

            return Ok(result);
        }

        #endregion
    }
}