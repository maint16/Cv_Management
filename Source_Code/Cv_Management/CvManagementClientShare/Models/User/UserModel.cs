﻿using System.Collections.Generic;
using CvManagementClientShare.Enums;
using CvManagementClientShare.Models.Hobby;
using CvManagementClientShare.Models.UserDescription;

namespace CvManagementClientShare.Models.User
{
    public class UserModel
    {
        #region Properties

        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public double Birthday { get; set; }

        public UserRoles Role { get; set; }

        /// <summary>
        ///     List of description.
        /// </summary>
        public IEnumerable<UserDescriptionModel> Descriptions { get; set; }

        /// <summary>
        ///     List of hobby
        /// </summary>
        public IEnumerable<HobbyModel> Hobbies { get; set; }

        #endregion
    }
}