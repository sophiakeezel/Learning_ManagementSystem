namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class StudentDetailView : ContentPage
{
    public StudentDetailView(Person student)
    {
        InitializeComponent();

        BindingContext = new StudentDetailViewModel(student);
    }

    private async void EnrolledCoursesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Course selectedCourse = e.SelectedItem as Course;
        await Shell.Current.Navigation.PushAsync(new StudentCourseView(selectedCourse.Id));
    }

}
