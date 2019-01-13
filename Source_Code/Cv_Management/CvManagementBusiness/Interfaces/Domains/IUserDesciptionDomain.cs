using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.UserDescription;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IUserDesciptionDomain
    {
        Task<SearchResultViewModel<IList<UserDescription>>> SearchUserDescriptionsAsync(
            SearchUserDescriptionViewModel condition, CancellationToken cancellationToken = default(CancellationToken));

        Task<UserDescription> AddUserDescriptionAsync(AddUserDescriptionViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UserDescription> EditUserDescriptionAsync(EditUserDescriptionViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteUserDescriptionAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

    }
}