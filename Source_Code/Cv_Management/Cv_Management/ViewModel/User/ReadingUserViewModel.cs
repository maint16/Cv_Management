﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cv_Management.ViewModel.User
{
    public class ReadingUserViewModel
    {
        public ReadingUserViewModel()
        {
            
        }
        public ReadingUserViewModel(Entities.User e )
        {
            this.Id = e.Id;
            this.FirstName = e.FirstName;
            this.LastName = e.LastName;
            this.Photo = e.Photo;
            this.Birthday = e.Birthday;
            this.Role = e.Role;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public double Birthday { get; set; }
        public string Role { get; set; }
    }
}