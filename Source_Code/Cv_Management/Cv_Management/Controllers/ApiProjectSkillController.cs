using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.ProjectSkill;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/projectSkill")]
    public class ApiProjectSkillController : ApiController
    {
        #region Properties

        private readonly IProjectSkillDomain _projectSkillDomain;

        #endregion

        #region Contructors

        public ApiProjectSkillController(IProjectSkillDomain projectSkillDomain)
        {
            _projectSkillDomain = projectSkillDomain;
        }

        #endregion

        #region Mothods

        /// <summary>
        ///     get projects skill using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchProjectSkills([FromBody] SearchProjectSkillViewModel model)
        {
            model = model ?? new SearchProjectSkillViewModel();
            Validate(model);

            var projectSkills = await _projectSkillDomain.SearchProjectSkillsAsync(model);

            return Ok(projectSkills);
        }

        #endregion
    }
}