﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}