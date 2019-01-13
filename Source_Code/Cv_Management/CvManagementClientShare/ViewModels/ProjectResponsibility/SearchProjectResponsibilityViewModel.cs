using System.Collections.Generic;

namespace CvManagementClientShare.ViewModels.ProjectResponsibility
{
    public class SearchProjectResponsibilityViewModel : BaseSearchViewModel
    {
        public HashSet<int> ProjectIds { get; set; }

        public HashSet<int> ResponsibilityIds { get; set; }
    }
}