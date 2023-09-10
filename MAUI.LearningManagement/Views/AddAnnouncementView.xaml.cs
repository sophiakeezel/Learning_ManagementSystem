namespace MAUI.LearningManagement.Views;

using System.Collections.ObjectModel;
using MAUI.Library.LearningManagement.Models;

public partial class AddAnnouncementView : ContentPage
{
    private Course _selectedCourse;
    private ObservableCollection<Announcement> _announcements;

    public AddAnnouncementView(Course selectedCourse)
	{
		InitializeComponent();

        _selectedCourse = selectedCourse;

        // Initialize the announcement picker
        _announcements = new ObservableCollection<Announcement>(_selectedCourse.Announcements);
        AnnouncementPicker.ItemsSource = _announcements;
    }

    private async void AddAnnouncementClicked(object sender, EventArgs e)
    {

        if (AddOrEditAnnouncementButton.Text == "Add Announcement")
        {
            //var selectedCourse = BindingContext as Course;

            // Get the data from the fields and create a new announcement
            var newAnnouncement = new Announcement
            {
                Name = NameEntry.Text,
                Description = DescriptionEditor.Text,
                CourseId = _selectedCourse.Id
            };

            // Add the new announcement to the selected course
            _selectedCourse.Announcements.Add(newAnnouncement);
            _announcements.Add(newAnnouncement);
        }
        else
        {
            // Edit an existing announcement
            var announcementToEdit = AnnouncementPicker.SelectedItem as Announcement;
            if (announcementToEdit != null)
            {
                announcementToEdit.Name = NameEntry.Text;
                announcementToEdit.Description = DescriptionEditor.Text;
            }
        }

        // Reset the fields
        AnnouncementPicker.SelectedItem = null;
        NameEntry.Text = string.Empty;
        DescriptionEditor.Text = string.Empty;
        AddOrEditAnnouncementButton.Text = "Add Announcement";
        // Navigate back to the previous page
        await Shell.Current.Navigation.PopAsync();

    }

    private void AnnouncementPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedAnnouncement = AnnouncementPicker.SelectedItem as Announcement;
        if (selectedAnnouncement != null)
        {
            NameEntry.Text = selectedAnnouncement.Name;
            DescriptionEditor.Text = selectedAnnouncement.Description;
            AddOrEditAnnouncementButton.Text = "Edit Announcement";
        }
    }

    private async void DeleteAnnouncementClicked(object sender, EventArgs e)
    {
        var selectedAnnouncement = AnnouncementPicker.SelectedItem as Announcement;
        if (selectedAnnouncement != null)
        {
            bool confirmDelete = await DisplayAlert("Delete Announcement", $"Are you sure you want to delete the \"{selectedAnnouncement.Name}\" announcement?", "Yes", "No");

            if (confirmDelete)
            {
                _selectedCourse.Announcements.Remove(selectedAnnouncement);
                _announcements.Remove(selectedAnnouncement);

                // Reset the fields
                AnnouncementPicker.SelectedItem = null;
                NameEntry.Text = string.Empty;
                DescriptionEditor.Text = string.Empty;
                AddOrEditAnnouncementButton.Text = "Add Announcement";
            }
        }
        else
        {
            await DisplayAlert("Error", "Please select an announcement to delete", "OK");
        }
    }

}
