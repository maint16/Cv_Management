using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.Models.User;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.User;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface IUserDomain
    {
        Task<SearchResultViewModel<IList<UserModel>>> SearchUserAsync(SearchUserViewModel conditions,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));


        Task<int> DeleteUserAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}