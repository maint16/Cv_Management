using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.ProjectSkill;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IProjectSkillDomain
    {
        Task<SearchResultViewModel<IList<ProjectSkill>>> SearchProjectSkillsAsync(SearchProjectSkillViewModel model, CancellationToken cancellationToken = default(CancellationToken));
    }
}