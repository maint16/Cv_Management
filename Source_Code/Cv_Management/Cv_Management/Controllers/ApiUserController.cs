using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel;
using Cv_Management.ViewModel.User;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/user")]
    public class ApiUserController : ApiController
    {
        #region Properties

        public readonly DbCvManagementContext DbSet;

        #endregion

        #region Constructors

        public ApiUserController()
        {
            DbSet = new DbCvManagementContext();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get users using specific conditions.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GellAll([FromBody] SearchUserViewModel model)
        {
            var result = new List<ReadingUserViewModel>();
            var users = DbSet.Users.AsQueryable();
            model = model ?? new SearchUserViewModel();
            if (model.Ids != null)
            {
                var ids = model.Ids.Where(x => x > 0).ToList();
                if (ids.Count > 0)
                    users = users.Where(x => ids.Contains(x.Id));

            }

            if (!string.IsNullOrEmpty(model.FirstName))
                users = users.Where(c => c.LastName.Contains(model.LastName));

            if (!string.IsNullOrEmpty(model.FirstName))
                users = users.Where(c => c.FirstName.Contains(model.FirstName));

            if (!string.IsNullOrEmpty(model.Role))
                users = users.Where(c => c.Role == model.Role);
            var results = new SearchResultViewModel<IList<User>>();
            results.Total = await users.CountAsync();
            var pagination = model.Pagination;
            if (pagination != null)
            {
                if (pagination.Page < 1)
                    pagination.Page = 1;
                users = users.Skip((pagination.Page - 1) * pagination.Records)
                    .Take(pagination.Records);
            }
            results.Records = await users.ToListAsync();
            return Ok(result);
        }
        /// <summary>
        /// Create User
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

            var userEnt = new User();
            MappingData(userEnt, model);

            userEnt = DbSet.Users.Add(userEnt);
            await DbSet.SaveChangesAsync();
            return Ok(new ReadingUserViewModel(userEnt));
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] UpdateUserViewModel model)
        {
            var user = DbSet.Users.Find(id);
            if (user == null)
                return NotFound();
            if (model == null)
            {
                model = new UpdateUserViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            MappingDataForUpdate(user, model);

            await DbSet.SaveChangesAsync();
            return Ok(new ReadingUserViewModel(user));
        }
        /// <summary>
        /// Delete User using Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            return Ok();
        }
        /// <summary>
        /// Mapping data from Entity to model for create
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
            entity.Role = model.Role;
        }
        /// <summary>
        /// Mapping data from entity to model for update
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
            entity.Role = model.Role;
        }

        #endregion
    }
}