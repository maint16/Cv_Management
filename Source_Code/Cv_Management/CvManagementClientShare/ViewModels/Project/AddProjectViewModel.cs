﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.Project
{
    public class AddProjectViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double StatedTime { get; set; }

        [Required]
        public double FinishedTime { get; set; }

        public HashSet<int> SkillIds { get; set; }

        public HashSet<int> ResponsibilityIds { get; set; }
    }
}