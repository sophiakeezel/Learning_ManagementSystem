using System;
using MAUI.LearningManagement.ViewModels;
using Microsoft.Maui.Controls;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(CourseId), "courseId")]

public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();
    }

    public int CourseId
    {
        get; set;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailViewModel(CourseId);
    }

}
