using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.UserDescription;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/userDescription")]
    public class ApiUserDescriptionController : ApiController
    {
        #region Properties

        private readonly IUserDesciptionDomain _userDesciptionDomain;

        #endregion

        #region Contructors

        public ApiUserDescriptionController(IUserDesciptionDomain userDesciptionDomain)
        {
            _userDesciptionDomain = userDesciptionDomain;
        }

        #endregion

        #region  Methods

        /// <summary>
        ///     Get user description using specific conditions
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> SearchUserDescriptions([FromBody] SearchUserDescriptionViewModel condition)
        {
            condition = condition ?? new SearchUserDescriptionViewModel();
            Validate(condition);

            var userDescriptions = await _userDesciptionDomain.SearchUserDescriptionsAsync(condition);

            return Ok(userDescriptions);
        }

        /// <summary>
        ///     Create User description
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddUserDescription([FromBody] AddUserDescriptionViewModel model)
        {
            if (model == null)
            {
                model = new AddUserDescriptionViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDescription = await _userDesciptionDomain.AddUserDescriptionAsync(model);

            return Ok(userDescription);
        }

        /// <summary>
        ///     Update User description
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> EditUserDescription([FromUri] int id,
            [FromBody] EditUserDescriptionViewModel model)
        {
            if (model == null)
            {
                model = new EditUserDescriptionViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userdescription = await _userDesciptionDomain.EditUserDescriptionAsync(model);

            return Ok(userdescription);
        }

        /// <summary>
        ///     Delete User Desciption from Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            var result = await _userDesciptionDomain.DeleteUserDescriptionAsync(id);

            return Ok(result);
        }

        #endregion
    }
}