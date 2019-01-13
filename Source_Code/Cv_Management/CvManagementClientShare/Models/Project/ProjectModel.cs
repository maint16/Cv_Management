using System.Collections.Generic;
using CvManagementClientShare.Models.Responsibility;
using CvManagementClientShare.Models.Skill;

namespace CvManagementClientShare.Models.Project
{
    public class ProjectModel
    {
        /// <summary>
        ///     Id of project.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Id of user that takes part in project.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Project name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Project description.
        /// </summary>
        public string Description { get; set; }

        public double StartedTime { get; set; }

        public double? FinishedTime { get; set; }

        public IEnumerable<SkillModel> Skills { get; set; }

        public IEnumerable<ResponsibilityModel> Responsibilities { get; set; }
    }
}