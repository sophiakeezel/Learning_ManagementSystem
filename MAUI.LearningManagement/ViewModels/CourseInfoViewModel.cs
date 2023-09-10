using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MAUI.LearningManagement.ViewModels
{
    public class CourseInfoViewModel : INotifyPropertyChanged
    {
        private int courseId;
        private Course course;
        public Course Course => course;

        private Student _loggedInStudent;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public string SemesterString { get; set; }
        public string Room { get; set; }
        public int CreditHours { get; set; }
        public string Code { get; set; }

        private Submission _selectedSubmission;
        public Submission SelectedSubmission
        {
            get => _selectedSubmission;
            set
            {
                _selectedSubmission = value;
                NotifyPropertyChanged();
            }
        }

        public CourseInfoViewModel(int courseId, Student loggedInStudent)
        {
            this.courseId = courseId;
            this._loggedInStudent = loggedInStudent;
            course = CourseService.Current.Courses.FirstOrDefault(c => c.Id == courseId);
            Name = course.Name;
            Description = course.Description;
            Prefix = course.Prefix;
            SemesterString = ClassToString(course.Semester);
            Room = course.Room;
            CreditHours = course.CreditHours;
            Code = course.Code;
        }

        public ObservableCollection<Announcement> Announcements => new ObservableCollection<Announcement>(course.Announcements);
        public ObservableCollection<Submission> Submissions => new ObservableCollection<Submission>(course.Submissions);
        public ObservableCollection<AssignmentGroup> AssignmentGroups => new ObservableCollection<AssignmentGroup>(course.AssignmentGroups);
        public ObservableCollection<Module> Modules => new ObservableCollection<Module>(course.Modules);
        public ObservableCollection<Person> Roster => new ObservableCollection<Person>(course.Roster);
        public ObservableCollection<Submission> StudentSubmissions
        {
            get => new ObservableCollection<Submission>(
                Submissions.Where(s => s.StudentId == _loggedInStudent.Id));
            set
            {
                _loggedInStudent.Submissions = value.ToList();
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateSubmissions()
        {
            NotifyPropertyChanged(nameof(StudentSubmissions));
            NotifyPropertyChanged(nameof(Submissions));
        }

        private string ClassToString(Term term)
        {
            var semesterString = string.Empty;
            switch (term)
            {
                case Term.Summer:
                    semesterString = "Summer";
                    break;
                case Term.Fall:
                    semesterString = "Fall";
                    break;
                case Term.Spring:
                    semesterString = "Spring";
                    break;
                default:
                    semesterString = "Fall";
                    break;
            }
            return semesterString;
        }

        private Term StringToClass(string s)
        {
            Term term;
            switch (s)
            {
                case "Fall":
                    term = Term.Fall;
                    break;
                case "Summer":
                    term = Term.Summer;
                    break;
                case "Spring":
                    term = Term.Spring;
                    break;
                default:
                    term = Term.Fall;
                    break;
            }
            return term;
        }
    }
}

