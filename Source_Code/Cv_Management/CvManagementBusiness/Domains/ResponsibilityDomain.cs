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
using CvManagementClientShare.ViewModels.Responsibility;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class ResponsibilityDomain : IResponsibilityDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public ResponsibilityDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods
       
        public async Task<SearchResultViewModel<IList<Responsibility>>> SearchResponsibilitiesAsync(SearchResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var responsibilities = _dbContext.Responsibilities.AsQueryable();
            if (model.Ids != null)
            {
                var ids = model.Ids.Where(x => x > 0).ToList();

                if (ids.Count > 0)
                    responsibilities = responsibilities.Where(x => ids.Contains(x.Id));
            }

            if (model.Names != null)
            {
                var names = model.Names.Where(c => !string.IsNullOrEmpty(c)).ToList();

                if (names.Count > 0)
                    responsibilities = responsibilities.Where(c => names.Contains(c.Name));
            }

            if (model.CreatedTime != null)
                responsibilities = responsibilities.Where(c => c.CreatedTime >= model.CreatedTime.From
                                                               && c.CreatedTime <= model.CreatedTime.To);

            if (model.LastModifiedTime != null)
                responsibilities = responsibilities.Where(c => c.LastModifiedTime >= model.LastModifiedTime.From
                                                               && c.LastModifiedTime <= model.LastModifiedTime.To);

            var result = new SearchResultViewModel<IList<Responsibility>>();
            result.Total = await responsibilities.CountAsync(cancellationToken);

            // Sort
            responsibilities =
                _dbService.Sort(responsibilities, SortDirection.Ascending, ResponsibilitySortProperty.Id);

            // Paging
            responsibilities = _dbService.Paginate(responsibilities, model.Pagination);

            result.Records = await responsibilities.ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Responsibility> AddResponsibilityAsync(AddResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Check exists responsibility
            var isExists = await _dbContext.Responsibilities.AnyAsync(c => c.Name == model.Name, cancellationToken);
            if (isExists)
               throw new Exception();

            //Inital responsibility object
            var responsibility = new Responsibility();
            responsibility.Name = model.Name;
            responsibility.CreatedTime = DateTime.Now.ToOADate();

            //Add responsibility to database
            responsibility = _dbContext.Responsibilities.Add(responsibility);

            //Save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return responsibility;
        }

        public async Task<Responsibility> EditResponsibilityAsync(EditResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //find Responsibility from database
            var responsibility = await _dbContext.Responsibilities.FindAsync(model.Id,cancellationToken);
            if (responsibility == null)
                throw new Exception();

            if (!string.IsNullOrEmpty(model.Name))
                responsibility.Name = model.Name;
            responsibility.LastModifiedTime = DateTime.Now.ToOADate();

            //Save changes to database
            await _dbContext.SaveChangesAsync(cancellationToken);

            return responsibility;
        }

        public async Task<int> DeleteResponsibilityAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Find responsibility in database
            var responsibility = await _dbContext.Responsibilities.FindAsync(id,cancellationToken);
            if (responsibility == null)
                throw new Exception();

            //Delete responsibility from database
            _dbContext.Responsibilities.Remove(responsibility);

            //Save changes in database
           var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion

    }
}