using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.Project;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/projet")]
    public class ApiProjectController : ApiController
    {
        #region Properties

        private readonly IProjectDomain _projectDomain;

        #endregion

        #region Contructors

        public ApiProjectController(IProjectDomain projectDomain)
        {
            _projectDomain = projectDomain;
        }

        #endregion

        #region Project

        /// <summary>
        ///     Get projects using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchProjects([FromBody] SearchProjectViewModel model)
        {
            model = model ?? new SearchProjectViewModel();
            Validate(model);

            var projects = await _projectDomain.SearchProjectAsync(model);

            return Ok(projects);
        }

        /// <summary>
        ///     Create Project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddProject([FromBody] AddProjectViewModel model)
        {
            if (model == null)
            {
                model = new AddProjectViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectDomain.AddProjectAsync(model);

            return Ok(project);
        }

        /// <summary>
        ///     EditProject Project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> EditProject([FromUri] int id, [FromBody] EditProjectViewModel model)
        {
            if (model == null)
            {
                model = new EditProjectViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectDomain.EditProjectAsync(model);

            return Ok(project);
        }

        /// <summary>
        ///     Delete project using Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteProject([FromUri] int id)
        {
            var result = await _projectDomain.DeleteProjectAsync(id);

            return Ok(result);
        }

        #endregion
    }
}