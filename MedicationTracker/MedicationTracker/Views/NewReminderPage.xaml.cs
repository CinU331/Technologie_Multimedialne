using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MedicationTracker.ViewModels;
using System;

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
            TimeSpanPicker.ItemsSource = viewModel.TimeSpans;
            TimeSpanPicker.SelectedItem = viewModel.TimeSpans[0];
        }

        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveReminder_Clicked(object sender, System.EventArgs e)
        {
            viewModel.NewReminder.TimeSpan = (TimeSpan)TimeSpanPicker.SelectedItem; //Powtarzanie nie do końca jeszcze działa. Przypomnienia się zapętlają gdy jest ustawione co innego niż TimeSpan 0
            await Navigation.PopModalAsync(); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Medicines.Count == 0) { viewModel.LoadMedicinesCommand.Execute(null); }
        }

        async private void LV_Medicines_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(viewModel.SelectedMedicine.ID=="Własny")
            {
                await Navigation.PushModalAsync(new NavigationPage(new CustomMedicinePage(viewModel, LV_Medicines)));
            }
        }

        private void ClickToShowPopup_Clicked(object sender, ClickedEventArgs e)
        {
        }
    }
}