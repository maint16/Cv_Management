using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.Models.Project;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.Project;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IProjectDomain
    {
        Task<SearchResultViewModel<IList<ProjectModel>>> SearchProjectAsync(SearchProjectViewModel condition, CancellationToken cancellationToken= default(CancellationToken));

        Task<Project> AddProjectAsync(AddProjectViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        Task<Project> EditProjectAsync(EditProjectViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteProjectAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
