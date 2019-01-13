using System.Collections.Generic;

namespace CvManagementClientShare.ViewModels.Hobby
{
    public class SearchHobbyViewModel : BaseSearchViewModel
    {
        /// <summary>
        ///     Id' hobbies
        /// </summary>
        public HashSet<int> Ids { get; set; }

        /// <summary>
        ///     UserIds that hobby belong to
        /// </summary>
        public HashSet<int> UserIds { get; set; }

        /// <summary>
        ///     Name' hobbies
        /// </summary>
        public HashSet<string> Names { get; set; }
    }
}