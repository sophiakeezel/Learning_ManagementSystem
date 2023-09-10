using MAUI.Library.LearningManagement.Models;
using Microsoft.Maui.Controls;
using System;

namespace MAUI.LearningManagement.Views
{
    public partial class AddAssignmentToGroupView : ContentPage
    {
        private AssignmentGroup _selectedAssignmentGroup;
        private Assignment _selectedAssignment;

        public AddAssignmentToGroupView(AssignmentGroup selectedAssignmentGroup)
        {
            InitializeComponent();
            _selectedAssignmentGroup = selectedAssignmentGroup;

            AssignmentListView.ItemsSource = _selectedAssignmentGroup.Assignments;     
        }

        private async void AddAssignmentClicked(object sender, EventArgs e)
        {
            // Get the data from the fields and create a new assignment
            var newAssignment = new Assignment
            {
                Name = AssignmentNameEntry.Text,
                Description = AssignmentDescriptionEntry.Text,
                TotalAvailablePoints = Convert.ToDecimal(TotalAvailablePointsEntry.Text),
                DueDate = DueDatePicker.Date
            };

            // Add the new assignment to the selected assignment group
            _selectedAssignmentGroup.Assignments.Add(newAssignment);

            // Clear the fields
            AssignmentNameEntry.Text = string.Empty;
            AssignmentDescriptionEntry.Text = string.Empty;
            TotalAvailablePointsEntry.Text = string.Empty;
            DueDatePicker.Date = DateTime.Today;

            // Navigate back to the previous page
            await Shell.Current.Navigation.PopAsync();
        }

        private void OnAssignmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedAssignment = e.SelectedItem as Assignment;
        }

        private async void EditAssignmentClicked(object sender, EventArgs e)
        {
            if (_selectedAssignment != null)
            {
                AssignmentNameEntry.Text = _selectedAssignment.Name;
                AssignmentDescriptionEntry.Text = _selectedAssignment.Description;
                TotalAvailablePointsEntry.Text = _selectedAssignment.TotalAvailablePoints.ToString();
                DueDatePicker.Date = _selectedAssignment.DueDate;

                _selectedAssignmentGroup.Assignments.Remove(_selectedAssignment);
            }
            else
            {
                await DisplayAlert("Error", "Please select an assignment to edit", "OK");
            }
        }

        private async void RemoveAssignmentClicked(object sender, EventArgs e)
        {
            if (_selectedAssignment != null)
            {
                bool confirmRemove = await DisplayAlert("Remove Assignment", $"Are you sure you want to remove the \"{_selectedAssignment.Name}\" assignment?", "Yes", "No");

                if (confirmRemove)
                {
                    _selectedAssignmentGroup.Assignments.Remove(_selectedAssignment);
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select an assignment to remove", "OK");
            }
        }

    }
}
