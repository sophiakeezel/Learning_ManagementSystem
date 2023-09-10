using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Library.LearningManagement.Models
{
    public class Student : Person
    {
        public float GPA { get; set; }

        public Dictionary<int, double> Grades { get; set; }

        public PersonClassification Classification { get; set; }

        public Student()
        {
            Grades = new Dictionary<int, double>();
            StudentCourses = new List<Course>();
            Submissions = new List<Submission>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }
    }

    public enum PersonClassification
    {
        Freshman, Sophomore, Junior, Senior
    }
}