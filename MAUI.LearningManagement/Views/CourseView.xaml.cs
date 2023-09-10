using MAUI.LearningManagement.ViewModels;
using MAUI.Library.LearningManagement.Models;
using Microsoft.Maui.Controls;

namespace MAUI.LearningManagement.Views
{
    public partial class CourseView : ContentPage
    {
        private CourseInfoViewModel _viewModel;

        public CourseView(int courseId)
        {
            InitializeComponent();
            _viewModel = new CourseInfoViewModel(courseId, null);
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
            // Navigate to a detail page for the selected module
            await Shell.Current.Navigation.PushAsync(new SubmissionGradingView(selectedSubmission, _viewModel));
        }

    }
}