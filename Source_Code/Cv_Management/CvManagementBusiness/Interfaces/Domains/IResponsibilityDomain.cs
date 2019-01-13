using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.Responsibility;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IResponsibilityDomain
    {
        Task<SearchResultViewModel<IList<Responsibility>>> SearchResponsibilitiesAsync(
            SearchResponsibilityViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        Task<Responsibility> AddResponsibilityAsync(AddResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Responsibility> EditResponsibilityAsync(EditResponsibilityViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteResponsibilityAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}