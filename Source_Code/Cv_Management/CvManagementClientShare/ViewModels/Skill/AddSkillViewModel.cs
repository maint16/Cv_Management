using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.Skill
{
    public class AddSkillViewModel
    {
        [Required] public string Name { get; set; }
    }
}