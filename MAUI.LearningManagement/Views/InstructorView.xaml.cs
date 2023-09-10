using MAUI.LearningManagement.ViewModels;
using MAUI.Library.LearningManagement.Models;
namespace MAUI.LearningManagement.Views;

public partial class InstructorView : ContentPage
{
    public InstructorView()
    {
        InitializeComponent();
        BindingContext = new InstructorViewViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    // Enrollments

    private void AddEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddEnrollmentClick(Shell.Current);
    }

    private void RemoveEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemoveEnrollmentClick();
    }

    private void EditEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddEnrollmentClick(Shell.Current);
    }

    private void AddEnrollmentToCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddToCourseClick(Shell.Current);
    }

    private void Toolbar_EnrollmentsClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ShowEnrollments();
    }

    // Courses

    private void AddCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }

    private void RemoveCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemoveCourseClick();
    }

    private void EditCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }

    private void ViewCourseClick(object sender, EventArgs e)
    {
        var instructorViewModel = BindingContext as InstructorViewViewModel;
        var courseId = instructorViewModel.SelectedCourse.Id;
        Shell.Current.Navigation.PushAsync(new CourseView(courseId));
    }

    private void Toolbar_CoursesClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ShowCourses();
    }

    private async void AddAnnouncementClick(object sender, EventArgs e)
    {
        var instructorViewModel = BindingContext as InstructorViewViewModel;
        await Shell.Current.Navigation.PushAsync(new AddAnnouncementView(instructorViewModel.SelectedCourse));
    }

    private async void AddAssignmentClick(object sender, EventArgs e)
    {
        var instructorViewModel = BindingContext as InstructorViewViewModel;
        await Shell.Current.Navigation.PushAsync(new AddAssignmentView(instructorViewModel.SelectedCourse));
    }

    private async void AddModuleClick(object sender, EventArgs e)
    {
        var instructorViewModel = BindingContext as InstructorViewViewModel;
        await Shell.Current.Navigation.PushAsync(new AddModuleView(instructorViewModel.SelectedCourse));
    }

    // Navigation

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RefreshView();
    }

}