using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.Responsibility;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/responsibility")]
    public class ApiResponsibilityController : ApiController
    {
        #region Properties 

        private readonly IResponsibilityDomain _responsibilityDomain;

        #endregion

        #region Contructors

        public ApiResponsibilityController(IResponsibilityDomain responsibilityDomain)
        {
            _responsibilityDomain = responsibilityDomain;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get Responsibilities using specific conditions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchResponsibilities([FromBody] SearchResponsibilityViewModel model)
        {
            model = model ?? new SearchResponsibilityViewModel();
            Validate(model);

            var responsibilities = await _responsibilityDomain.SearchResponsibilitiesAsync(model);

            return Ok(responsibilities);
        }

        /// <summary>
        ///     Create responsibility
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddResponsibility([FromBody] AddResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new AddResponsibilityViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responsibility = await _responsibilityDomain.AddResponsibilityAsync(model);

            return Ok(responsibility);
        }

        /// <summary>
        ///     Update responsibility
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> EditResponsibility([FromUri] int id,
            [FromBody] EditResponsibilityViewModel model)
        {
            if (model == null)
            {
                model = new EditResponsibilityViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responsibility = await _responsibilityDomain.EditResponsibilityAsync(model);

            return Ok(responsibility);
        }

        /// <summary>
        ///     Delete responsibility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteResponsibility([FromUri] int id)
        {
            var result = await _responsibilityDomain.DeleteResponsibilityAsync(id);

            return Ok(result);
        }

        #endregion
    }
}