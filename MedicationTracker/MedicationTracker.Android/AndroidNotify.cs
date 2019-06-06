using Android.App;
using Android.Content;
using Android.OS;
using MedicationTracker.Droid;
using MedicationTracker.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidNotify))]
namespace MedicationTracker.Droid
{
    class AndroidNotify : INotify
    {
        public void Notification(string title, string text)
        {
            Notification.Builder builder;
            // Instantiate the builder and set notification elements:
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
                builder = new Notification.Builder(Android.App.Application.Context)
#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
                    .SetContentTitle(title).SetContentText(text)
                    .SetSmallIcon(Resource.Drawable.pill)
                    .SetDefaults(NotificationDefaults.Lights | NotificationDefaults.Vibrate);
            }
            else
            {
#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
                builder = new Notification.Builder(Android.App.Application.Context)
#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
                    .SetContentTitle(title).SetContentText(text)
                    .SetSmallIcon(Resource.Drawable.pill)
                    .SetChannelId("notification")
                    .SetDefaults(NotificationDefaults.Lights | NotificationDefaults.Vibrate);
            }

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
            Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}