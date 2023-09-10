namespace MAUI.LearningManagement.Views;
using MAUI.Library.LearningManagement.Models;

public partial class ModuleDetailView : ContentPage
{
    public ModuleDetailView(Module selectedModule)
    {
        InitializeComponent();
        BindingContext = selectedModule;
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}
    