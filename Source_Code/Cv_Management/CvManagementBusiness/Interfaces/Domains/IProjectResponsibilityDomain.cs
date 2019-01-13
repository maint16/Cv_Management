using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.ProjectResponsibility;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IProjectResponsibilityDomain
    {

        Task<SearchResultViewModel<IList<ProjectResponsibility>>> SearchProjectResponsibilityAsync(
            SearchProjectResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

      
    }
}
