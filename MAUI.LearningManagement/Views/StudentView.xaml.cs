namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
        InitializeComponent();
        BindingContext = new StudentViewViewModel();
    }

    private async void EnterClicked(object sender, EventArgs e)
    {
        if (int.TryParse(StudentIdEntry.Text, out int id))
        {
            var viewModel = BindingContext as StudentViewViewModel;
            var student = await viewModel.GetStudentById(id);

            if (student != null)
            {
                await Navigation.PushAsync(new StudentDetailView(student));
            }
            else
            {
                await DisplayAlert("Error", $"No student found with the Id '{StudentIdEntry.Text}'.", "OK");
            }
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}
