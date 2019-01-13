﻿using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.PersonalSkill
{
    public class AddPersonalSkillViewModel
    {
        [Required] public int SkillCategoryId { get; set; }

        [Required] public int SkillId { get; set; }

        public int Point { get; set; }
    }
}