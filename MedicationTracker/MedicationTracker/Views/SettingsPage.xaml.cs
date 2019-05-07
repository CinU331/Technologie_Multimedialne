using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicationTracker.ViewModels;

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
	}
}