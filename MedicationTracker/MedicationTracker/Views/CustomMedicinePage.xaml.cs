using MedicationTracker.Models;
using MedicationTracker.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomMedicinePage : ContentPage
	{
        NewReminderViewModel NewReminderViewModel { get; set; }

		public CustomMedicinePage (NewReminderViewModel newReminderViewModel)
		{
			InitializeComponent ();
            NewReminderViewModel = newReminderViewModel;
            NameEntry.Text = NewReminderViewModel.SelectedMedicine.Name;
            DescEntry.Text = NewReminderViewModel.SelectedMedicine.Description;
        }

        private async void DodajButton_Clicked(object sender, EventArgs e)
        {

            NewReminderViewModel.SelectedMedicine.Name = NameEntry.Text;
            NewReminderViewModel.SelectedMedicine.Description = DescEntry.Text;
            await Navigation.PopModalAsync();
        }

        private async void AnulujButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}