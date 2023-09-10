using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;

namespace MAUI.LearningManagement.ViewModels
{
    class CourseDetailViewModel : INotifyPropertyChanged
    {

        // Course properties
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public string SemesterString { get; set; }
        public string Room { get; set; }
        public int CreditHours { get; set; }
        public string Code { get; set; }
        public List<Person> Roster { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        

        // Constructor
        public CourseDetailViewModel(int id = 0 )
        {
            if (id > 0)
            {
                LoadById(id);
            }
        }
    

        // Load course if not new 
        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var course = CourseService.Current.GetByCourseId(id) as Course;
            if (course != null)
            {
                Name = course.Name;
                Id = course.Id;
                SemesterString = ClassToString(course.Semester);
                Description = course.Description;
                CreditHours = course.CreditHours;
                Code = course.Code;
                Room = course.Room;
                Roster = course.Roster;
                Prefix = course.Prefix;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(CreditHours));
            NotifyPropertyChanged(nameof(Code));
            NotifyPropertyChanged(nameof(Id));
            NotifyPropertyChanged(nameof(SemesterString));
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

        public Person SelectedPerson { get; set; }

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

        // Course Manager

        public void AddCourse()
        {
            if (Id <= 0)
            {
                CourseService.Current.Add(new Course() { Name = Name, Description = Description, Prefix = Prefix,
                CreditHours = CreditHours, Room = Room, Semester = StringToClass(SemesterString)});
            }
            else
            {
                var refToUpdate = CourseService.Current.GetByCourseId(Id) as Course;
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.CreditHours = CreditHours;
                refToUpdate.Semester = StringToClass(SemesterString);
                refToUpdate.Room = Room;
                refToUpdate.Prefix = Prefix;
            }

            Shell.Current.GoToAsync("//Instructor");
        }

        // Semester 

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

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Room));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(CreditHours));
            NotifyPropertyChanged(nameof(Code));
            NotifyPropertyChanged(nameof(SemesterString));
        }
    }
}