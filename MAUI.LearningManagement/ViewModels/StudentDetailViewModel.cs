using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;

namespace MAUI.LearningManagement.ViewModels
{
    public class StudentDetailViewModel : INotifyPropertyChanged
    {
        private Person _student;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public Person Student
        {
            get => _student;
            set
            {
                if (_student != value)
                {
                    _student = value;
                    OnPropertyChanged();
                }
            }
        }

        public StudentDetailViewModel(Person student)
        {
            Student = student;
            _studentService = StudentService.Current;
            _courseService = CourseService.Current;
            UpdatePastCourses(); 

            EnrolledCourses = new ObservableCollection<Course>(Student.StudentCourses);
            UpdateAvailableCourses();
        }

        private void UpdateAvailableCourses()
        {
            var enrolledCourseIds = EnrolledCourses.Select(c => c.Id);
            AvailableCourses = new ObservableCollection<Course>(_courseService.Courses.Where(c => !enrolledCourseIds.Contains(c.Id)));
        }

        public ObservableCollection<Course> AvailableCourses { get; private set; }
        public ObservableCollection<Course> EnrolledCourses { get; private set; }

        public ICommand EnrollCommand => new Command<Course>(Enroll);
        public ICommand DropCommand => new Command<Course>(Drop);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Enroll(Course course)
        {
            if (course == null)
                return;

            EnrolledCourses.Add(course);
            _courseService.AddtoRoster(Student, course);
            UpdateAvailableCourses();
            OnPropertyChanged(nameof(AvailableCourses));
        }

        private void Drop(Course course)
        {
            if (course == null)
                return;

            EnrolledCourses.Remove(course);
            _courseService.RemovefromRoster(Student as Student, course);
            UpdateAvailableCourses();
            OnPropertyChanged(nameof(AvailableCourses));
        }

        public ObservableCollection<Course> PastCourses { get; private set; }

        private void UpdatePastCourses()
        {
            DateTime currentDate = DateTime.Now;
            PastCourses = new ObservableCollection<Course>(Student.StudentCourses.Where(c => c.EndDate < currentDate));
        }


    }

}

