namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Services;
using MAUI.Library.LearningManagement.Models;

public partial class SubmissionDetailView : ContentPage
{
    private Submission _submission;
    private Student _student;

    public SubmissionDetailView(Submission submission)
	{
		InitializeComponent();
        _submission = submission;
        _student = StudentService.Current.GetById(_submission.StudentId) as Student;
        BindingContext = _submission;
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
