using MAUI.Library.LearningManagement.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace MAUI.LearningManagement.Views
{
    public partial class AddModuleView : ContentPage
    {
        private Course _selectedCourse;
        private List<ContentItem> _contentItems;
        private ObservableCollection<Module> _modules;

        public AddModuleView(Course selectedCourse)
        {
            InitializeComponent();
            _selectedCourse = selectedCourse;
            _contentItems = new List<ContentItem>();

            _modules = new ObservableCollection<Module>(_selectedCourse.Modules);
            ModulePicker.ItemsSource = _modules;
            ContentTypePicker.SelectedIndexChanged += ContentTypePicker_SelectedIndexChanged;
        }

        private void ContentTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContentTypePicker.SelectedIndex != -1 && ContentTypePicker.SelectedItem.ToString() == "File")
            {
                ContentPathEntry.IsVisible = true;
            }
            else
            {
                ContentPathEntry.IsVisible = false;
            }
        }

        private async void AddModuleClicked(object sender, EventArgs e)
        {
            // Get the data from the fields and create a new module
            var newModule = new Module
            {
                Name = NameEntry.Text,
                Description = DescriptionEditor.Text
            };

            // Add the new module to the selected course
            _selectedCourse.Modules.Add(newModule);

            // Navigate back to the previous page
            await Shell.Current.Navigation.PopAsync();
        }

        private async void EditModuleClicked(object sender, EventArgs e)
        {
            var selectedModule = ModulePicker.SelectedItem as Module;

            if (selectedModule != null)
            {
                // Load the module details into the fields
                NameEntry.Text = selectedModule.Name;
                DescriptionEditor.Text = selectedModule.Description;

                // Update the "Add Module" button text and event handler
                AddModuleButton.Text = "Update Module";
                AddModuleButton.Clicked -= AddModuleClicked;
                AddModuleButton.Clicked += UpdateModuleClicked;
            }
            else
            {
                await DisplayAlert("Error", "Please select a module to edit", "OK");
            }
        }

        private async void UpdateModuleClicked(object sender, EventArgs e)
        {
            var selectedModule = ModulePicker.SelectedItem as Module;

            if (selectedModule != null)
            {
                _selectedCourse.EditModule(selectedModule.Id, NameEntry.Text, DescriptionEditor.Text);

                // Refresh the picker
                _modules.Clear();
                foreach (var module in _selectedCourse.Modules)
                {
                    _modules.Add(module);
                }

                // Reset the fields and button
                NameEntry.Text = string.Empty;
                DescriptionEditor.Text = string.Empty;
                AddModuleButton.Text = "Add Module";
                AddModuleButton.Clicked -= UpdateModuleClicked;
                AddModuleButton.Clicked += AddModuleClicked;
            }
            else
            {
                await DisplayAlert("Error", "Please select a module to edit", "OK");
            }
        }

        private async void DeleteModuleClicked(object sender, EventArgs e)
        {
            var selectedModule = ModulePicker.SelectedItem as Module;

            if (selectedModule != null)
            {
                bool confirmDelete = await DisplayAlert("Delete Module", $"Are you sure you want to delete the \"{selectedModule.Name}\" module?", "Yes", "No");

                if (confirmDelete)
                {
                    _selectedCourse.DeleteModule(selectedModule.Id);

                    // Refresh the picker
                    _modules.Clear();
                    foreach (var module in _selectedCourse.Modules)
                    {
                        _modules.Add(module);
                    }

                    // Reset the fields
                    NameEntry.Text = string.Empty;
                    DescriptionEditor.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a module to delete", "OK");
            }
        }


        private void AddContentClicked(object sender, EventArgs e)
        {
            string contentType = ContentTypePicker.SelectedItem?.ToString();
            string contentName = ContentNameEntry.Text;
            string contentDescription = ContentDescriptionEntry.Text;

            if (contentType != null && !string.IsNullOrEmpty(contentName) && !string.IsNullOrEmpty(contentDescription))
            {
                ContentItem contentItem = null;

                switch (contentType)
                {
                    case "Assignment":
                        contentItem = new AssignmentItem
                        {
                            Name = contentName,
                            Description = contentDescription,
                            Assignment = new Assignment { Name = contentName }
                        };
                        break;
                    case "Page":
                        contentItem = new PageItem
                        {
                            Name = contentName,
                            Description = contentDescription
                        };
                        break;
                    case "File":
                        string filePath = ContentPathEntry.Text;
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            contentItem = new FileItem
                            {
                                Name = contentName,
                                Description = contentDescription,
                                Path = filePath
                            };
                        }
                        break;
                }

                if (contentItem != null)
                {
                    _contentItems.Add(contentItem);
                    ContentNameEntry.Text = string.Empty;
                    ContentDescriptionEntry.Text = string.Empty;
                }
                else
                {
                    DisplayAlert("Error", "Please enter valid content information", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Please enter all required fields", "OK");
            }
        }
    }
}

