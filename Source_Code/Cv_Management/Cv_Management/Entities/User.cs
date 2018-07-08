using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cv_Management.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public double Birthday { get; set; }
        public string Role { get; set; }
        public List<SkillCategory> SkillCategorys { get; set; }
        public  List<UserDescription> UserDescriptions { get; set; }
        public  List<Project> Projects { get; set; }
    }
}