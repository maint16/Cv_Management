using System.Collections.Generic;
using CvManagementClientShare.Models;

namespace CvManagementClientShare.ViewModels.Responsibility
{
    public class SearchResponsibilityViewModel : BaseSearchViewModel
    {
        public HashSet<int> Ids { get; set; }

        public HashSet<string> Names { get; set; }

        public RangeModel<double, double> CreatedTime { get; set; }

        public RangeModel<double, double> LastModifiedTime { get; set; }
    }
}