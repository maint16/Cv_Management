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
using CvManagementClientShare.ViewModels.ProjectSkill;
using CvManagementModel.Models;
using CvManagementModel.Models.Context;

namespace CvManagementBusiness.Domains
{
    public class ProjectSkillDomain :IProjectSkillDomain
    {
        #region Properties

        private readonly CvManagementDbContext _dbContext;
        private readonly IDbService _dbService;

        #endregion

        #region Contructor

        public ProjectSkillDomain(DbContext dbContext, IDbService dbService)
        {
            _dbContext = (CvManagementDbContext)dbContext;
            _dbService = dbService;
        }

        #endregion

        #region Methods

        public async Task<SearchResultViewModel<IList<ProjectSkill>>> SearchProjectSkillsAsync(SearchProjectSkillViewModel model,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Get project skills list
            var projectSkills = _dbContext.ProjectSkills.AsQueryable();
            if (model.ProjectIds != null)
            {
                var projectIds = model.ProjectIds.Where(x => x > 0).ToList();
                if (projectIds.Count > 0)
                    projectSkills = projectSkills.Where(x => projectIds.Contains(x.ProjectId));
            }
            if (model.SkillIds != null)
            {
                var skillIds = model.SkillIds.Where(x => x > 0).ToList();
                if (skillIds.Count > 0)
                    projectSkills = projectSkills.Where(x => skillIds.Contains(x.SkillId));
            }

            //Result search 
            var result = new SearchResultViewModel<IList<ProjectSkill>>();
            result.Total = await projectSkills.CountAsync(cancellationToken);

            //Do sort
            projectSkills = _dbService.Sort(projectSkills, SortDirection.Ascending, ProjectSkillSortProperty.Id);

            //Do pagination
            projectSkills = _dbService.Paginate(projectSkills, model.Pagination);

            result.Records = await projectSkills.ToListAsync(cancellationToken);

            return result;
        }

        #endregion

    }
}
