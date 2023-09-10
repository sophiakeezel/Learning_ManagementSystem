using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace MAUI.LearningManagement.Views
{
    public partial class AddSubmissionView : ContentPage
    {
        private Course _selectedCourse;
        private Submission _selectedSubmission;
        private CourseInfoViewModel _viewModel;
        private Student _loggedinStudent;

        public AddSubmissionView(CourseInfoViewModel viewModel, Student loggedinstudent)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _selectedCourse = viewModel.Course;
            AssignmentGroupPicker.ItemsSource = _selectedCourse.AssignmentGroups;
            _loggedinStudent = loggedinstudent;
        }

        private void AssignmentGroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedGroup = AssignmentGroupPicker.SelectedItem as AssignmentGroup;
            if (selectedGroup != null)
            {
                AssignmentPicker.ItemsSource = selectedGroup.Assignments;
            }
            else
            {
                AssignmentPicker.ItemsSource = null;
            }
        }

        private async void SaveSubmissionClicked(object sender, EventArgs e)
        {
            if (_selectedSubmission == null)
            {
                var selectedAssignment = AssignmentPicker.SelectedItem as Assignment;
                var newSubmission = new Submission
                {
                    Student = _loggedinStudent,
                    StudentId = _loggedinStudent.Id,
                    Assignment = selectedAssignment,
                    Content = SubmissionContentEditor.Text,
                    Grade = 0 
                };

                _viewModel.Submissions.Add(newSubmission);
                _selectedCourse.Submissions.Add(newSubmission);
            }
            else
            {
                _selectedSubmission.Assignment = AssignmentPicker.SelectedItem as Assignment;
                _selectedSubmission.Content = SubmissionContentEditor.Text;
                _selectedSubmission.Grade = 0;
            }

            _viewModel.UpdateSubmissions();
            await Shell.Current.Navigation.PopAsync();
        }

        private void AssignmentPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedAssignment = AssignmentPicker.SelectedItem as Assignment;
            _selectedSubmission = _selectedCourse.Submissions.Find(submission => submission.Assignment == selectedAssignment);

            if (_selectedSubmission != null)
            {
                SubmissionContentEditor.Text = _selectedSubmission.Content;
                SaveSubmissionButton.Text = "Update Submission";
            }
            else
            {
                SubmissionContentEditor.Text = string.Empty;
                SaveSubmissionButton.Text = "Add Submission";
            }
        }

    }
}