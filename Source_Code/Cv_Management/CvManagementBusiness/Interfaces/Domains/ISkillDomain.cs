using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CvManagementClientShare.ViewModels;
using CvManagementClientShare.ViewModels.Skill;
using CvManagementModel.Models;

namespace CvManagementBusiness.Interfaces.Domains
{
    public interface ISkillDomain
    {

        Task<SearchResultViewModel<IList<Skill>>> SearchSkillsAsync(SearchSkillViewModel condition,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Skill> AddSkillAsync(AddSkillViewModel model, CancellationToken cancellationToken = default(CancellationToken));

        Task<Skill> EditSkillAsync(EditSkillViewModel model,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteSkillAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}