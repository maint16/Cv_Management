using System.Threading.Tasks;
using System.Web.Http;
using CvManagementClientShare.ViewModels.ProjectResponsibility;
using CvManagementModel.Models.Context;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/projectResponsibility")]
    public class ApiProjectResponsibilityController : ApiController
    {
        #region properties

        public readonly CvManagementDbContext DbSet;

        #endregion

        #region Contructors

        public ApiProjectResponsibilityController()
        {
            DbSet = new CvManagementDbContext();
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
        public async Task<IHttpActionResult> Search([FromBody] SearchProjectResponsibilityViewModel model)
        {
            return Ok();
        }

        /// <summary>
        ///     Create project responsibility
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateProjectResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new CreateProjectResponsibilityViewModel();
                Validate(model);
            }

            return Ok();
        }

        /// <summary>
        ///     Delete project responsibility
        /// </summary>
        /// <param name="responsibilityId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete([FromUri] int responsibilityId, [FromUri] int projectId)
        {
            return Ok();
        }

        #endregion
    }
}