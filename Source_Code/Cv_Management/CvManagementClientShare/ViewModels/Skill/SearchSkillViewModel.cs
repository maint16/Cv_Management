using System.Collections.Generic;
using CvManagementClientShare.Models;

namespace CvManagementClientShare.ViewModels.Skill
{
    public class SearchSkillViewModel : BaseSearchViewModel
    {
        #region Properties

        /// <summary>
        ///     Skill'id indexes
        /// </summary>
        public HashSet<int> Ids { get; set; }

        /// <summary>
        ///     Skill'name indexes
        /// </summary>
        public HashSet<string> Names { get; set; }

        public RangeModel<double, double> StartedTime { get; set; }

        #endregion
    }
}