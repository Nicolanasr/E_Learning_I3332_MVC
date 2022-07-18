using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Learning_I3332_MVC.Models
{
    public class CourseList
    {

        public List<Courses> Courses { get; set; }
        public SelectList TeacherID { get; set; }
        public int TeacherIdint { get; set; }
        public string SearchString { get; set; }

    }
}

