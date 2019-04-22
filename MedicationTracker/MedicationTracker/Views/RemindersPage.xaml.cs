using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicationTracker.Models;
using MedicationTracker.ViewModels;

namespace MedicationTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RemindersPage : ContentPage
	{
        RemindersViewModel viewModel;

		public RemindersPage()
		{
			InitializeComponent();
            BindingContext = viewModel = new RemindersViewModel();
		}

        async void OnReminderSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Reminder reminder)) { return; }

            await Navigation.PushAsync(new ReminderDetailPage(new ReminderDetailViewModel(reminder)));

            // Manually deselect item.
            RemindersListView.SelectedItem = null;
        }

        async void AddReminder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewReminderPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Reminders.Count == 0) { viewModel.LoadRemindersCommand.Execute(null); }
        }
    }
}