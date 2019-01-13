using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.Skill
{
    public class EditSkillViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}