using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MAUI.LearningManagement.ViewModels
{

    public class StudentViewViewModel
    {
        private StudentService _studentService;

        public StudentViewViewModel()
        {
            _studentService = StudentService.Current;
        }

        public async Task<Person> GetStudentById(int id )
        {
            if (id <= 0)
            {
                return null;
            }

            var student = _studentService.GetById(id);
            Console.WriteLine(student.Name);

            if (student == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No student found with the id '{id}'.", "OK");
            }

            return student;
        }
    }

}



