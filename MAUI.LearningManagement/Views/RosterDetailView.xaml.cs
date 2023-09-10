namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

public partial class RosterDetailView : ContentPage
{
    private InstructorViewViewModel _viewModel;
    private Person _selectedPerson;

    public RosterDetailView(InstructorViewViewModel viewModel, Person selectedPerson)
	{
        InitializeComponent();
        _viewModel = viewModel;
        _selectedPerson = selectedPerson;
        CoursesListView.ItemsSource = _viewModel.Courses;

    }

    private async void CoursesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Course selectedCourse = e.SelectedItem as Course;

        selectedCourse.Roster.Add(_selectedPerson);
        _selectedPerson.StudentCourses.Add(selectedCourse);

        _viewModel.RefreshView();
        await Shell.Current.Navigation.PopAsync();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

}
