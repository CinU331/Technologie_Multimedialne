using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationTracker.Views.TutorialPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page6 : ContentPage
	{
		public Page6 ()
		{
			InitializeComponent ();
		}
        async private void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}