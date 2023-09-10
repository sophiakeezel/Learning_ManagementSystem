using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUI.LearningManagement.ViewModels
{
    public class InstructorViewViewModel : INotifyPropertyChanged
    {
        public InstructorViewViewModel()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
        }

        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService
                    .Current
                    .Students
                    .Where(
                    s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);

            }
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                return new ObservableCollection<Course>(CourseService.Current.Courses);
            }
        }

        public string Title { get => "Instructor / Administrator Menu"; }

        public bool IsEnrollmentsVisible
        {
            get; set;
        }

        public bool IsCoursesVisible
        {
            get; set;
        }

        public void ShowEnrollments()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public void ShowCourses()
        {
            IsEnrollmentsVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Enrollments

        public void AddEnrollmentClick(Shell s)
        {
            var idParam = SelectedPerson?.Id ?? 0;
            s.GoToAsync($"//PersonDetail?personId={idParam}");
        }

        public void RemoveEnrollmentClick()
        {
            if (SelectedPerson == null) { return; }

            StudentService.Current.Remove(SelectedPerson);
            RefreshView();
        }

        public async void AddToCourseClick(Shell s)
        {
            if (SelectedPerson == null) { return; }

            await s.Navigation.PushAsync(new RosterDetailView(this, SelectedPerson));
        
            RefreshView();
        }

        // Courses

        public void AddCourseClick(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//CourseDetail?courseId={idParam}");
        }

        public void ViewCourseInfo(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//CourseInfo?courseId={idParam}");
        }

        public void RemoveCourseClick()
        {
            if (SelectedCourse == null) { return; }

            CourseService.Current.Remove(SelectedCourse);
            RefreshView();
        }

       
        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
            NotifyPropertyChanged(nameof(SelectedCourse));
        }

    }
}