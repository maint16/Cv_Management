using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.User;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/user")]
    public class ApiUserController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiUserController()
        {
            DbSet = new DbCvManagementContext();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GellAll([FromBody] SearchUserViewModel model)
        {
            var result = new List<ReadingUserViewModel>();
            var users = DbSet.Users.AsQueryable();
            model= model ?? new SearchUserViewModel();
            if (model.Id > 0)
                users = users.Where(c => c.Id == model.Id);

            if (!string.IsNullOrEmpty(model.FirstName))
                users = users.Where(c => c.LastName.Contains(model.LastName));

            if (!string.IsNullOrEmpty(model.FirstName))
                users = users.Where(c => c.FirstName.Contains(model.FirstName));

            if (!string.IsNullOrEmpty(model.Role))
                users = users.Where(c => c.Role == model.Role);

            result = users.Select(c => new ReadingUserViewModel()
            {
                Id=c.Id,
                Birthday = c.Birthday,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Photo = c.Photo,
                Role = c.Role
            }).ToList();
            return Ok(result);
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] CreateUserViewModel model)
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
            DbSet.SaveChanges();
            return Ok(new ReadingUserViewModel(userEnt));
        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody] UpdateUserViewModel model )
        {
            var user = DbSet.Users.Find(id);
            if (user == null)
                return NotFound();
            if (model == null)
            {
                model= new UpdateUserViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            MappingDataForUpdate(user, model);

            DbSet.SaveChanges();
            return Ok(new ReadingUserViewModel(user));
        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            return Ok();
        }

        #region Common function

        public void MappingData(User entity, CreateUserViewModel model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Birthday = model.Birthday;
            if (model.Photo != null)
                entity.Photo = Convert.ToBase64String(model.Photo.Buffer);
            entity.Role = model.Role;
        }
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