using MAUI.Library.LearningManagement.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace MAUI.LearningManagement.Views
{
    public partial class AddAssignmentView : ContentPage
    {
        private Course _selectedCourse;
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }

        public AddAssignmentView(Course selectedCourse)
        {
            InitializeComponent();
            _selectedCourse = selectedCourse;

            // Initialize the AssignmentGroups ObservableCollection
            AssignmentGroups = new ObservableCollection<AssignmentGroup>(_selectedCourse.AssignmentGroups);

            // Set the Picker's ItemSource to the AssignmentGroups
            AssignmentGroupPicker.ItemsSource = AssignmentGroups;
        }

        private async void AddAssignmentGroupClicked(object sender, EventArgs e)
        {
            // Get the data from the fields and create a new assignment group
            var newAssignmentGroup = new AssignmentGroup
            {
                Name = NameEntry.Text,
                Weight = Convert.ToDecimal(WeightEntry.Text)
            };

            // Add the new assignment group to the selected course
            _selectedCourse.AssignmentGroups.Add(newAssignmentGroup);

            // Navigate back to the previous page
            await Shell.Current.Navigation.PopAsync();
        }

        private async void AddAssignmentToGroupClicked(object sender, EventArgs e)
        {
            // Get the selected assignment group
            AssignmentGroup selectedAssignmentGroup = AssignmentGroupPicker.SelectedItem as AssignmentGroup;

            if (selectedAssignmentGroup != null)
            {
                // Navigate to the AddAssignmentToGroupView page
                await Shell.Current.Navigation.PushAsync(new AddAssignmentToGroupView(selectedAssignmentGroup));
            }
            else
            {
                // Show a message if no assignment group is selected
                await DisplayAlert("Error", "Please select an assignment group", "OK");
            }
        }

        private async void EditAssignmentGroupClicked(object sender, EventArgs e)
        {
            var selectedAssignmentGroup = AssignmentGroupPicker.SelectedItem as AssignmentGroup;
            if (selectedAssignmentGroup != null)
            {
                NameEntry.Text = selectedAssignmentGroup.Name;
                WeightEntry.Text = (selectedAssignmentGroup.Weight * 100).ToString();
                AssignmentGroups.Remove(selectedAssignmentGroup);
                _selectedCourse.AssignmentGroups.Remove(selectedAssignmentGroup);
            }
            else
            {
                await DisplayAlert("Error", "Please select an assignment group to edit", "OK");
            }
        }

        private async void DeleteAssignmentGroupClicked(object sender, EventArgs e)
        {
            var selectedAssignmentGroup = AssignmentGroupPicker.SelectedItem as AssignmentGroup;
            if (selectedAssignmentGroup != null)
            {
                bool confirmDelete = await DisplayAlert("Delete Assignment Group", $"Are you sure you want to delete the \"{selectedAssignmentGroup.Name}\" assignment group?", "Yes", "No");

                if (confirmDelete)
                {
                    AssignmentGroups.Remove(selectedAssignmentGroup);
                    _selectedCourse.AssignmentGroups.Remove(selectedAssignmentGroup);
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select an assignment group to delete", "OK");
            }
        }

    }
}
