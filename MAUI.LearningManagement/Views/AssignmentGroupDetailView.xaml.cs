namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;

public partial class AssignmentGroupDetailView : ContentPage
{
	public AssignmentGroupDetailView(AssignmentGroup selectedAssignmentGroup)
	{
		InitializeComponent();
        BindingContext = selectedAssignmentGroup;
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
