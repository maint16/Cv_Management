using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.Responsibility
{
    public class AddResponsibilityViewModel
    {
        [Required] public string Name { get; set; }
    }
}