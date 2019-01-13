using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.UserDescription
{
    public class EditUserDescriptionViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}