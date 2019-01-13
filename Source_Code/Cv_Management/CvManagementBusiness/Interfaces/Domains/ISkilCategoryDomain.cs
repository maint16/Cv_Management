using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.Models.SkillCategory;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.SkillCategory;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface ISkilCategoryDomain
    {
        #region Methods

        Task<SearchResultViewModel<IList<SkillCategoryModel>>> SearchSkillCategoriesAsync(
            SearchSkillCategoryViewModel conditions, CancellationToken cancellationToken = default(CancellationToken));

        Task<SkillCategory> AddSkillCategoryAsync(AddSkillCategoryViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));


        Task<SkillCategory> EditCategoryAsync(EditSkillCategoryViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}