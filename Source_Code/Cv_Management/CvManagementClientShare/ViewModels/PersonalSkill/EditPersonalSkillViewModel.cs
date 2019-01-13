using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.PersonalSkill
{
    public class EditPersonalSkillViewModel
    {
        [Required] public int SkillCategoryId { get; set; }

        [Required] public int SkillId { get; set; }

        public int Point { get; set; }
    }
}