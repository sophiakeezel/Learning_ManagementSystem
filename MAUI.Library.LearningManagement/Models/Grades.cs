using System;
namespace MAUI.Library.LearningManagement.Models
{
	public class Grades
	{
        public int creditHours { get; set; }
        public Dictionary<AssignmentGroup, float> Points { get; set; }
        public float Grade { get; set; }

        public Grades(Course c)
        {
            creditHours = c.CreditHours;
            Points = new Dictionary<AssignmentGroup, float>();
            Grade = 0;
        }
    }
}

