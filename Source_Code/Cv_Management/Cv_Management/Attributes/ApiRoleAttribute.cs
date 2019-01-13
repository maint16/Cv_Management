using System.Web.Http;
using System.Web.Http.Controllers;
using CvManagementModel.Models.Context;

namespace CvManagement.Attributes
{
    public class ApiRoleAttribute : AuthorizeAttribute
    {
        public readonly CvManagementDbContext DbSet;
        public string[] _roles;

        public ApiRoleAttribute(string[] roles)
        {
            _roles = roles;
            DbSet = new CvManagementDbContext();
        }

        public override void OnAuthorization(HttpActionContext context)
        {
        }
    }
}