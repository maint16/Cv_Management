using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cv_Management.ViewModel.ProjectSkill
{
    public class SearchProjectSkillViewModel
    {
        public int ProjectId { get; set; }

        public int SkillId { get; set; }
    }
}