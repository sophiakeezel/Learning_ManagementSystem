using MAUI.LearningManagement.ViewModels;
using MAUI.Library.LearningManagement.Models;
using MAUI.Library.LearningManagement.Services;
using Microsoft.Maui.Controls;
namespace MAUI.LearningManagement.Views;

public partial class StudentCourseView : ContentPage
{
    private CourseInfoViewModel _viewModel;
    private Student _loggedInStudent;
    private Submission _selectedSubmission;

    public StudentCourseView(int courseId)
	{
        InitializeComponent();
        _loggedInStudent = new Student(); 
        _viewModel = new CourseInfoViewModel(courseId, _loggedInStudent);
        BindingContext = _viewModel;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private async void AnnouncementsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Announcement selectedAnnouncement = e.SelectedItem as Announcement;
        // Navigate to a detail page for the selected announcement
        await Shell.Current.Navigation.PushAsync(new AnnouncementDetailView(selectedAnnouncement));
    }

    private async void AssignmentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        AssignmentGroup selectedAssignmentGroup = e.SelectedItem as AssignmentGroup;
        // Navigate to a detail page for the selected assignment group
        await Shell.Current.Navigation.PushAsync(new AssignmentGroupDetailView(selectedAssignmentGroup));
    }

    private async void ModulesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Module selectedModule = e.SelectedItem as Module;
        // Navigate to a detail page for the selected module
        await Shell.Current.Navigation.PushAsync(new ModuleDetailView(selectedModule));
    }

    private async void SubmissionsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Submission selectedSubmission = e.SelectedItem as Submission;

        if (selectedSubmission.StudentId == _loggedInStudent.Id)
        {
            await Shell.Current.Navigation.PushAsync(new SubmissionDetailView(selectedSubmission));
        }
        else
        {
            await DisplayAlert("Error", "You are not authorized to view this submission.", "OK");
        }
    }

    private async void AddSubmissionClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new AddSubmissionView(_viewModel, _loggedInStudent));
        _viewModel.UpdateSubmissions();
    }

    private void RemoveSubmissionClicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedSubmission != null)
        {
            _viewModel.Submissions.Remove(_viewModel.SelectedSubmission);
            _viewModel.UpdateSubmissions();
        }
    }

}
