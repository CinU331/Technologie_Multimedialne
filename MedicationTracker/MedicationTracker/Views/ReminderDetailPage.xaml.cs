using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MedicationTracker.ViewModels;

namespace MedicationTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReminderDetailPage : ContentPage
	{
        private ReminderDetailViewModel viewModel;

        public ReminderDetailPage()
        {
            InitializeComponent();
        }

		public ReminderDetailPage(ReminderDetailViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = this.viewModel = viewModel;
		}
	}
}