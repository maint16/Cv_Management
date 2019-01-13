using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.Responsibility
{
    public class EditResponsibilityViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}