﻿using System.Collections.Generic;

namespace CvManagementClientShare.ViewModels.SkillCategory
{
    public class SearchSkillCategoryViewModel
    {
        #region Properties

        /// <summary>
        ///     List of indexes.
        /// </summary>
        public HashSet<int> Ids { get; set; }

        /// <summary>
        ///     List of user indexes.
        /// </summary>
        public HashSet<int> UserIds { get; set; }

        /// <summary>
        ///     Skill category names.
        /// </summary>
        public HashSet<string> Names { get; set; }

        /// <summary>
        ///     Whether personal skills are included in search result or not.
        /// </summary>
        public bool IncludeSkills { get; set; }

        /// <summary>
        ///     Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        #endregion
    }
}