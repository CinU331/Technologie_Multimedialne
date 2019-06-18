using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamd.ImageCarousel.Forms.Plugin.Abstractions;

namespace MedicationTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorialPage : ContentPage
    {
        public ImageCarousel ImageCarousel { get; set; }

		public TutorialPage ()
		{
			InitializeComponent ();

            var images = new ObservableCollection<FileImageSource> {
              new FileImageSource { File = @"C:\Technologie_Multimedialne\MedicationTracker\MedicationTracker\Resources\pills.png" }
            };

            ImageCarousel = new ImageCarousel(images);// Nie działa. W ogóle nie wyświetla zdjęć
            ImageCarouselControl = ImageCarousel;

            //ImageControl.Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png");
        }
	}
}