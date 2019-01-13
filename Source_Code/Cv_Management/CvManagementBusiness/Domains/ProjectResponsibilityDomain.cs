using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CvManagementBusiness.Interfaces.Domains;
using CvManagementBusiness.Interfaces.Services;
using CvManagementClientShare.Enums.SortProperties;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.ProjectResponsibility;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
   public class ProjectResponsibilityDomain :IProjectResponsibilityDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public ProjectResponsibilityDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods

        public async Task<SearchResultViewModel<IList<ProjectResponsibility>>> SearchProjectResponsibilityAsync(SearchProjectResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Get list projet responsibility
            var projectResponsibilities = _dbContext.ProjectResponsibilities.AsQueryable();
            if (model.ProjectIds != null)
            {
                var projectIds = model.ProjectIds.Where(x => x > 0).ToList();
                if (projectIds.Count > 0)
                    projectResponsibilities = projectResponsibilities.Where(x => projectIds.Contains(x.ProjectId));
            }
            if (model.ResponsibilityIds != null)
            {
                var responsibilityIds = model.ResponsibilityIds.Where(x => x > 0).ToList();
                if (responsibilityIds.Count > 0)
                    projectResponsibilities =
                        projectResponsibilities.Where(x => responsibilityIds.Contains(x.ResponsibilityId));
            }

            var result = new SearchResultViewModel<IList<ProjectResponsibility>>();
            result.Total = await projectResponsibilities.CountAsync(cancellationToken);

            //Do sort
            projectResponsibilities =
                _dbService.Sort(projectResponsibilities, SortDirection.Ascending, ProjectSortProperty.Id);

            //Do paginatin
            projectResponsibilities = _dbService.Paginate(projectResponsibilities, model.Pagination);

            result.Records = await projectResponsibilities.ToListAsync(cancellationToken);

            return result;
        }

        #endregion

    }
}
