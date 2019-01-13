using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.ProjectResponsibility;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/projectResponsibility")]
    public class ApiProjectResponsibilityController : ApiController
    {
        #region properties

        private readonly IProjectResponsibilityDomain _projectResponsibilityDomain;

        #endregion

        #region Contructors

        public ApiProjectResponsibilityController(IProjectResponsibilityDomain projectResponsibilityDomain)
        {
            _projectResponsibilityDomain = projectResponsibilityDomain;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get project responsibility using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchProjectResponsibilities(
            [FromBody] SearchProjectResponsibilityViewModel model)
        {
            model = model ?? new SearchProjectResponsibilityViewModel();
            Validate(model);

            var projectResponsibilities = await _projectResponsibilityDomain.SearchProjectResponsibilityAsync(model);

            return Ok(projectResponsibilities);
        }

        #endregion
    }
}