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
        ListView ListView { get; set; }

		public CustomMedicinePage (NewReminderViewModel newReminderViewModel, ListView listView)
		{
			InitializeComponent ();
            NewReminderViewModel = newReminderViewModel;
            ListView = listView;
            NameEntry.Text = NewReminderViewModel.SelectedMedicine.Name;
            DescEntry.Text = NewReminderViewModel.SelectedMedicine.Description;
        }

        private async void DodajButton_Clicked(object sender, EventArgs e)
        {
            //NewReminderViewModel.Medicines.Remove(NewReminderViewModel.SelectedMedicine);
            Medicine medicine = new Medicine
            {
                ID = "Własny",
                Name = NameEntry.Text,
                Description = DescEntry.Text,
                Image = new Image { Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png") }
            };
            //NewReminderViewModel.Medicines.Add(medicine);
            //NewReminderViewModel.SelectedMedicine = medicine;
            NewReminderViewModel.SelectedMedicine.Name = NameEntry.Text;
            NewReminderViewModel.SelectedMedicine.Description = DescEntry.Text;
            ListView.ItemsSource = null;
            ListView.ItemsSource = NewReminderViewModel.Medicines;
            ListView.SelectedItem = NewReminderViewModel.SelectedMedicine;
            await Navigation.PopModalAsync();
        }

        private async void AnulujButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}