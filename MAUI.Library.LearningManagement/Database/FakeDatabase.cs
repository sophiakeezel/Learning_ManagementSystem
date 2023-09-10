using System;
using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;

namespace MAUI.Library.LearningManagement.Database
{
	public static class FakeDatabase
	{
        private static List<Course> courses = new List<Course>();
        private static List<Person> people = new List<Person>();
        private static List<Student> students = new List<Student>();
        private static List<AssignmentGroup> assignmentGroups = new List<AssignmentGroup>();

        public static List<AssignmentGroup> AssignmentGroups
        {
            get
            {
                return assignmentGroups;
            }
        }

        public static List<Person> People
        {
            get
            {
                return people;
            }
        }

        public static List<Course> Courses
        {
            get
            {
                return courses;
            }
        }

        public static List<Student> Students
        {
            get
            {
                return students;
            }
        }

    }
}

