using System;
using System.Threading.Tasks;
using System.Web.Http;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementClientShare.ViewModels.User;
using CvManagementModel.Models;

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
        ///     Create User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateUserViewModel model)
        {
            if (model == null)
            {
                model = new CreateUserViewModel();
                Validate(model);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok();
        }

        /// <summary>
        ///     Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] UpdateUserViewModel model)
        {
            return Ok();
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

        /// <summary>
        ///     Mapping data from Entity to model for create
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        public void MappingData(User entity, CreateUserViewModel model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Birthday = model.Birthday;
            if (model.Photo != null)
                entity.Photo = Convert.ToBase64String(model.Photo.Buffer);
            // entity.Role = model.Role;
        }

        /// <summary>
        ///     Mapping data from entity to model for update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        public void MappingDataForUpdate(User entity, UpdateUserViewModel model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Birthday = model.Birthday;
            if (model.Photo != null)
                entity.Photo = Convert.ToBase64String(model.Photo.Buffer);
            //  entity.Role = model.Role;
        }

        #endregion
    }
}