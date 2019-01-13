using System.Collections.Generic;

namespace CvManagementClientShare.ViewModels.UserDescription
{
    public class SearchUserDescriptionViewModel
    {
        #region Properties

        /// <summary>
        ///     User description indexes.
        /// </summary>
        public HashSet<int> Ids { get; set; }

        /// <summary>
        ///     User index which description belongs to.
        /// </summary>
        public HashSet<int> UserIds { get; set; }

        /// <summary>
        ///     Pagination information.
        /// </summary>
        public Pagination Pagination { get; set; }

        #endregion
    }
}