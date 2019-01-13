using System.ComponentModel.DataAnnotations;

namespace CvManagementClientShare.ViewModels.ProjectResponsibility
{
    public class AddProjectResponsibilityViewModel
    {
        [Required] public int ProjectId { get; set; }

        [Required] public int ResponsibilityId { get; set; }
    }
}