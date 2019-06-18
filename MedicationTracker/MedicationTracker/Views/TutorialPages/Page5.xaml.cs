﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationTracker.Views.TutorialPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page5 : ContentPage
	{
		public Page5 ()
		{
			InitializeComponent ();
		}
        async private void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Page6()));
        }
    }
}