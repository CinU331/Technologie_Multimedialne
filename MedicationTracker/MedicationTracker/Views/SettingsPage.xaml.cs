using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicationTracker.ViewModels;
using Xamd.ImageCarousel.Forms.Plugin.Abstractions;
using System.Collections.ObjectModel;

namespace MedicationTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        async private void TutorialButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TutorialPage()));
        }
    }
}