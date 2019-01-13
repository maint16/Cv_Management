using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.UserDescription;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class UserDescriptionDomain : IUserDesciptionDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public UserDescriptionDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods

        public async Task<SearchResultViewModel<IList<UserDescription>>> SearchUserDescriptionsAsync(SearchUserDescriptionViewModel condition,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var userDescriptions = _dbContext.UserDescriptions.AsQueryable();

            if (condition.Ids != null && condition.Ids.Count > 0)
            {
                var ids = condition.Ids.Where(x => x > 0).ToList();
                if (ids.Count > 0)
                    userDescriptions = userDescriptions.Where(userDescription => ids.Contains(userDescription.Id));
            }

            if (condition.UserIds != null && condition.UserIds.Count > 0)
            {
                var userIds = condition.UserIds.Where(x => x > 0).ToList();
                if (userIds.Count > 0)
                    userDescriptions =
                        userDescriptions.Where(userDescription => userIds.Contains(userDescription.UserId));
            }

            var loadUserDescriptionResult = new SearchResultViewModel<IList<UserDescription>>();
            loadUserDescriptionResult.Total = await userDescriptions.CountAsync(cancellationToken);

            // Do sorting.
            userDescriptions =
                _dbService.Sort(userDescriptions, SortDirection.Ascending, UserDescriptionSortProperty.Id);

            // Do pagination.
            userDescriptions = _dbService.Paginate(userDescriptions, condition.Pagination);

            loadUserDescriptionResult.Records = await userDescriptions.ToListAsync(cancellationToken);

            return loadUserDescriptionResult;
        }

        public async Task<UserDescription> AddUserDescriptionAsync(AddUserDescriptionViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Get user profile.
            var profile = _dbContext.Users.FindAsync(model.Id, cancellationToken);
            if (profile == null)
                throw new Exception();

            // Add user description into database.
            var userDescription = new UserDescription();
            userDescription.UserId = profile.Id;
            userDescription.Description = model.Description;

            // Add the description into database.
            userDescription = _dbContext.UserDescriptions.Add(userDescription);

            // Save changes into database.
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userDescription;
        }

        public async Task<UserDescription> EditUserDescriptionAsync(EditUserDescriptionViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            // Find the user description by using id.
            var userDescription = await _dbContext.UserDescriptions.FindAsync(model.Id, cancellationToken);

            // Find the first record.
            if (userDescription == null)
                throw new Exception();

            userDescription.Description = model.Description;

            // Save changes.
            await _dbContext.SaveChangesAsync(cancellationToken);

            return userDescription;
        }

        public async Task<int> DeleteUserDescriptionAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            // Find the user description in the database.
            var userDescription = await _dbContext.UserDescriptions.FindAsync(id,cancellationToken);
            if (userDescription == null)
                throw new Exception();

            // Delete the description from database.
            _dbContext.UserDescriptions.Remove(userDescription);

            // Save changes in database.
           var result= await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }


        #endregion
    }
}