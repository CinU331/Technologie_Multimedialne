using Android.Content.Res;
using Android.Media;

using Xamarin.Forms;

using MedicationTracker.Droid;
using MedicationTracker.Interfaces;

[assembly: Dependency(typeof(AndroidAudio))]
namespace MedicationTracker.Droid
{
    public class AndroidAudio : IAudio
    {
        public void PlayAudioFile(string file)
        {
            MediaPlayer player = new MediaPlayer();
            AssetFileDescriptor fileDescription = Android.App.Application.Context.Assets.OpenFd(file);

            player.Prepared += (s, e) =>
            {
                player.Start();
            };
            player.SetDataSource(fileDescription.FileDescriptor, fileDescription.StartOffset, fileDescription.Length);
            player.Prepare();
        }
    }
}