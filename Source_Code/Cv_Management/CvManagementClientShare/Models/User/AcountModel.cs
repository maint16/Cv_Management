using CvManagementClientShare.Enums;

namespace CvManagementClientShare.Models.User
{
    public class AcountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRoles Role { get; set; }
    }
}