using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cv_Management.ViewModel.User
{
    public class SearchUserViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
      
        public string Role { get; set; }
    }
}