using System.Collections.Generic;
using CvManagementClientShare.Models.Skill;

namespace CvManagementClientShare.Models.SkillCategory
{
    public class SkillCategoryModel
    {
        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public double CreatedTime { get; set; }

        /// <summary>
        ///     List of skills belongs to skill
        /// </summary>
        public IEnumerable<SkillCategorySkillRelationshipModel> Skills { get; set; }

        #endregion
    }
}