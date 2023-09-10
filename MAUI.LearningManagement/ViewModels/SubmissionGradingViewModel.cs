using System.ComponentModel;
using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;
using System.Runtime.CompilerServices;

namespace MAUI.LearningManagement.ViewModels
{
    public class SubmissionGradingViewModel : INotifyPropertyChanged
    {
        private Submission _submission;
        private CourseInfoViewModel _courseInfoViewModel;
        private Student selectedStudent;

        public SubmissionGradingViewModel(Submission submission, CourseInfoViewModel courseInfoViewModel)
        {
            _submission = submission;
            _courseInfoViewModel = courseInfoViewModel;
        }

        public string StudentName => _submission.Student.Name;
        public string AssignmentName => _submission.Assignment.Name;

        public decimal Grade
        {
            get => _submission.Grade;
            set
            {
                _submission.Grade = value;
                NotifyPropertyChanged();
            }
        }

        public void SaveGrade()
        {
            _courseInfoViewModel.UpdateSubmissions();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

