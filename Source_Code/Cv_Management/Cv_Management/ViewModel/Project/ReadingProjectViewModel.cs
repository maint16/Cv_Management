﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cv_Management.ViewModel.Project
{
    public class ReadingProjectViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StatedTime { get; set; }
        public double? FinishedTime { get; set; }
    }
}