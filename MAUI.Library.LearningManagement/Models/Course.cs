using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Library.LearningManagement.Models
{
    public class Course : IEquatable<Course>
    {
        public string Code
        {
            get
            {
                return $"[{Id}]{Prefix}";
            }
        }

        private static int lastId = 0;
        public string Prefix { get; set; }
        public int Id
        {
            get; private set;
        }

        public string Name { get; set; }
       
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Person> Roster { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }

        public IEnumerable<Assignment> Assignments
        {
            get
            {
                return AssignmentGroups.SelectMany(ag => ag.Assignments);
            }
        }

        public List<Submission> Submissions { get; set; }
        public List<Module> Modules { get; set; }
        public List<Announcement> Announcements { get; set; }
        public int CreditHours { get; set; }
        public string Room { get; set; }

        public Course()
        {
            Name = Name ?? string.Empty;
            Description = Description ?? string.Empty;
            Roster = new List<Person>();
            Room = Room ?? string.Empty;
            AssignmentGroups = new List<AssignmentGroup>();
            Submissions = new List<Submission>();
            Modules = new List<Module>();
            Announcements = new List<Announcement>();
            Prefix = Prefix ?? string.Empty;
            Id = ++lastId;
            CreditHours = CreditHours;
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }

        public string DetailDisplay
        {
            get
            {
                return $"{ToString()}\n{Description}\n\n" +
                    $"Announcements:\n{string.Join("\n\t", Announcements.Select(s => s.ToString()).ToArray())}\n\n" +
                    $"Roster:\n{string.Join("\n\t", Roster.Select(s => s.ToString()).ToArray())}\n\n" +
                    $"Assignments:\n{string.Join("\n\t", AssignmentGroups.Select(a => a.ToString()).ToArray())}\n\n" +
                    $"Modules:\n{string.Join("\n\t", Modules.Select(m => m.ToString()).ToArray())}";
            }
        }

        private Term _semester;
        public Term Semester
        {
            get => _semester;
            set
            {
                _semester = value;
                SetStartAndEndDates();
            }
        }

        private void SetStartAndEndDates()
        {
            int year = DateTime.Now.Year;

            switch (_semester)
            {
                case Term.Spring:
                    StartDate = new DateTime(year, 1, 1);
                    EndDate = new DateTime(year, 4, 30);
                    break;
                case Term.Summer:
                    StartDate = new DateTime(year, 5, 1);
                    EndDate = new DateTime(year, 7, 31);
                    break;
                case Term.Fall:
                    StartDate = new DateTime(year, 8, 1);
                    EndDate = new DateTime(year, 12, 31);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_semester), _semester, null);
            }
        }


        public bool EditModule(int moduleId, string newName, string newDescription)
        {
            var module = Modules.FirstOrDefault(m => m.Id == moduleId);
            if (module != null)
            {
                module.Name = newName;
                module.Description = newDescription;
                return true;
            }
            return false;
        }

        public bool DeleteModule(int moduleId)
        {
            var module = Modules.FirstOrDefault(m => m.Id == moduleId);
            if (module != null)
            {
                Modules.Remove(module);
                return true;
            }
            return false;
        }

        public bool Equals(Course other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }

    public enum Term
    {
        Spring, Summer, Fall
    }
}