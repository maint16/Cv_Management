using System.ComponentModel.DataAnnotations;
using ApiMultiPartFormData.Models;

namespace CvManagementClientShare.ViewModels.SkillCategory
{
    public class AddSkillCategoryViewModel
    {
        [Required]
        public int UserId { get; set; }

        public HttpFile Photo { get; set; }

        [Required]
        public string Name { get; set; }
    }
}