using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CvManagementClientShare.ViewModels.Project
{
    public class EditProjectViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double StatedTime { get; set; }

        public double? FinishedTime { get; set; }

        public HashSet<int> SkillIds { get; set; }

        public HashSet<int> ResponsibilityIds { get; set; }
    }
}