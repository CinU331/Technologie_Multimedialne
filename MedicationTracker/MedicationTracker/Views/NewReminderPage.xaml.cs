using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicationTracker.ViewModels;

namespace MedicationTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewReminderPage : ContentPage
	{
        NewReminderViewModel viewModel;

        public NewReminderPage()
		{
			InitializeComponent();
            BindingContext = viewModel = new NewReminderViewModel();
        }

        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveReminder_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Medicines.Count == 0) { viewModel.LoadMedicinesCommand.Execute(null); }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(viewModel.CustomMedicine.Name.Length != 0)
            {
                LV_Medicines.IsEnabled = false;
                LV_Medicines.SelectedItem = null;
            }
            else
            {
                LV_Medicines.IsEnabled = true;
            }
        }
    }
}