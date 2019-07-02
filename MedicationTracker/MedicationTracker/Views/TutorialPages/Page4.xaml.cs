using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationTracker.Views.TutorialPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page4 : ContentPage
	{
		public Page4 ()
		{
			InitializeComponent ();
		}

        async private void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page5());
        }
    }
}