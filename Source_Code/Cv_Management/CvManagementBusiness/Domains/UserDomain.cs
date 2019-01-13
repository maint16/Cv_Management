using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.Models.Hobby;
using CvManagementClientShare.Models.User;
using CvManagementClientShare.Models.UserDescription;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.User;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class UserDomain : IUserDomain
    {
        #region Contructor

        public UserDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext) dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Methods

        public async Task<SearchResultViewModel<IList<UserModel>>> SearchUserAsync(SearchUserViewModel condition,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var users = _dbContext.Users.AsQueryable();

            // Ids are defined.
            if (condition.Ids != null)
            {
                var ids = condition.Ids.Where(c => c > 0).ToList();
                if (ids.Count > 0)
                    users = users.Where(c => condition.Ids.Contains(c.Id));
            }

            // Last names are defined.
            if (condition.LastNames != null)
            {
                var lastNames = condition.LastNames.Where(c => !string.IsNullOrEmpty(c)).ToList();
                if (lastNames.Count > 0)
                    users = users.Where(c => lastNames.Contains(c.LastName));
            }

            // First names are defined.
            if (condition.FirstNames != null)
            {
                var firstNames = condition.FirstNames.Where(c => !string.IsNullOrEmpty(c)).ToList();
                if (firstNames.Count > 0)
                    users = users.Where(c => firstNames.Contains(c.FirstName));
            }

            // Birthday range is defined.
            if (condition.Birthday != null)
            {
                var birthday = condition.Birthday;
                if (birthday.From != null)
                    users = users.Where(c => c.Birthday >= birthday.From);

                if (birthday.To != null)
                    users = users.Where(user => user.Birthday <= birthday.To);
            }

            //Todo get user base on role of current user.
            users = users.Where(x => x.Status == UserStatuses.Active);

            #region Search user descriptions && hobbies

            //user descriptions
            var userDescriptions = Enumerable.Empty<UserDescription>().AsQueryable();

            if (condition.IncludeDescriptions)
                userDescriptions = _dbContext.UserDescriptions.AsQueryable();

            // Get all hobbies.
            var hobbies = Enumerable.Empty<Hobby>().AsQueryable();
            if (condition.IncludeHobbies)
                hobbies = _dbContext.Hobbies.AsQueryable();

            var loadedUsers = from user in users
                select new UserModel
                {
                    Id = user.Id,
                    Birthday = user.Birthday,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photo = user.Photo,
                    Role = user.Role,
                    Descriptions = from description in userDescriptions
                        select new UserDescriptionModel
                        {
                            Id = description.Id,
                            Description = description.Description,
                            UserId = description.UserId
                        },
                    Hobbies = from hobby in hobbies
                        select new HobbyModel
                        {
                            Id = hobby.Id,
                            Name = hobby.Name,
                            UserId = hobby.UserId,
                            Description = hobby.Description
                        }
                };

            #endregion

            var result = new SearchResultViewModel<IList<UserModel>>();
            result.Total = await users.CountAsync();

            // Do sort
            loadedUsers = _dbService.Sort(loadedUsers, SortDirection.Ascending, UserSortProperty.Id);

            // Do pagination
            loadedUsers = _dbService.Paginate(loadedUsers, condition.Pagination);

            result.Records = await loadedUsers.ToListAsync();

            return result;
        }

        public Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUserAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}