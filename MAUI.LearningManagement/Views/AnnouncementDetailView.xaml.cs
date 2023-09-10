namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;

public partial class AnnouncementDetailView : ContentPage
{
	public AnnouncementDetailView(Announcement selectedAnnouncement)
	{
		InitializeComponent();
        BindingContext = selectedAnnouncement;
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
