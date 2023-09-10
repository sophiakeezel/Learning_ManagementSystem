using MAUI.LearningManagement.ViewModels;
using MAUI.Library.LearningManagement.Services;
using MAUI.Library.LearningManagement.Models;

namespace MAUI.LearningManagement.Views;

public partial class SubmissionGradingView : ContentPage
{
    private SubmissionGradingViewModel _viewModel;

    public SubmissionGradingView(Submission submission, CourseInfoViewModel courseInfoViewModel)
    {
        InitializeComponent();
        _viewModel = new SubmissionGradingViewModel(submission, courseInfoViewModel);
        BindingContext = _viewModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.SaveGrade();
        await Shell.Current.Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
