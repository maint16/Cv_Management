using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cv_Management.ViewModel.User
{
    public class SearchUserViewModel:BaseSearchViewModel
    {

        public HashSet<int> Ids { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
      
        public string Role { get; set; }
    }
}