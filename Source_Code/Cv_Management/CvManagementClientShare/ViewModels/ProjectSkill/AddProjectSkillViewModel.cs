using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.ProjectSkill
{
    public class AddProjectSkillViewModel
    {
        [Required] public int ProjectId { get; set; }

        [Required] public int SkillId { get; set; }
    }
}