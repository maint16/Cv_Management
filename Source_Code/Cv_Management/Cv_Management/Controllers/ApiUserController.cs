using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.User;

namespace CvManagement.Controllers
{
    [RoutePrefix("api/user")]
    public class ApiUserController : ApiController
    {
        #region Properties

        private readonly IUserDomain _userDomain;

        #endregion

        #region Constructors

        public ApiUserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Get users using specific conditions.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> SearchUser([FromBody] SearchUserViewModel model)
        {
            if (model == null)
            {
                model = new SearchUserViewModel();
                Validate(model);
            }

            var users = await _userDomain.SearchUserAsync(model);

            return Ok(users);
        }


        /// <summary>
        ///     Delete User using Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            return Ok();
        }

        #endregion
    }
}