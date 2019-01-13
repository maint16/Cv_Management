using System.Collections.Generic;

namespace CvManagementClientShare.ViewModels.SkillCategorySkillRelationship
{
    public class AddHasSkillViewModel
    {
        #region Properties

        /// <summary>
        ///     Skill category id.
        /// </summary>
        public int SkillCategoryId { get; set; }

        /// <summary>
        ///     List of skills
        /// </summary>
        public HashSet<HasSkillViewModel> HasSkills { get; set; }

        #endregion
    }
}