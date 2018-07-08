using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Cv_Management.Entities;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.User;
using Cv_Management.ViewModel.UserDescription;

namespace Cv_Management.Controllers
{
    [RoutePrefix("api/userDescription")]
    public class ApiUserDescriptionController : ApiController
    {
        public readonly DbCvManagementContext DbSet;

        public ApiUserDescriptionController()
        {
            DbSet= new DbCvManagementContext(); 
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult Search([FromBody]SearchUserDescriptionViewModel model)
        {
            model = model ?? new SearchUserDescriptionViewModel();
            var userDescriptions = DbSet.UserDescriptions.AsQueryable();
            if (model.Id > 0)
                userDescriptions = userDescriptions.Where(c => c.Id == model.Id);
            if (model.UserId > 0)
                userDescriptions = userDescriptions.Where(c => c.UserId == model.UserId);
            if (!string.IsNullOrEmpty(model.Description))
                userDescriptions = userDescriptions.Where(c => c.Description.Contains(model.Description));
            var result = userDescriptions.Select(c => new ReadingUserDescriptionViewModel()
            {
               Id=c.Id,
               UserId = c.UserId,
               User = new ReadingUserViewModel()
               {
                   Id=c.User.Id,
                   FirstName = c.User.FirstName,
                   LastName = c.User.LastName,
                   Birthday = c.User.Birthday,
                   Role = c.User.Role
               },
               Description = c.Description

            }).ToList();
            return Ok(result);

        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]CreateUserDescriptionViewModel model)
        {
            if (model == null)
            {
                model = new CreateUserDescriptionViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userDescription = new UserDescription();
            userDescription.UserId = model.UserId;
            userDescription.Description = model.Description;
            userDescription = DbSet.UserDescriptions.Add(userDescription);
            DbSet.SaveChanges();
            return Ok(userDescription);

        }
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromUri] int id, [FromBody]UpdateUserDescriptionViewModel model)
        {
            if (model == null)
            {
                model = new UpdateUserDescriptionViewModel();
                Validate(model);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //get UserDescription
            var userDescription = DbSet.UserDescriptions.Find(id);
            if (userDescription == null)
                return NotFound();
            userDescription.UserId = model.UserId;
            userDescription.Description = model.Description;
            DbSet.SaveChanges();
            return Ok(userDescription);

        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var userDescription = DbSet.UserDescriptions.Find(id);
            if (userDescription == null)
                return NotFound();
            DbSet.UserDescriptions.Remove(userDescription);
            DbSet.UserDescriptions.Remove(userDescription);
            return Ok();

        }
    }
}