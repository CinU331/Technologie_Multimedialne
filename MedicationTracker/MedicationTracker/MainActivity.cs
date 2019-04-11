using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace MedicationTracker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        ListView listView;
        List<Reminder> mlist;
        ReminderAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            mlist = new List<Reminder>
            {
                new Reminder
                {
                    Medicine = "Apap",
                    DateTime = DateTime.Now
                },
                new Reminder
                {
                    Medicine = "Ibum",
                    DateTime = DateTime.Now
                },
                new Reminder
                {
                    Medicine = "Tabex",
                    DateTime = DateTime.Now
                }
            };
            listView = FindViewById<ListView>(Resource.Id.listView1);
            adapter = new ReminderAdapter(this, mlist);
            listView.Adapter = adapter;
            listView.ItemClick += ListView_ItemClick;

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var select = mlist[e.Position].Medicine;
            Toast toast = Toast.MakeText(this, select, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom, 0, 200);
            toast.Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    listView.Visibility = ViewStates.Visible;
                    return true;
                case Resource.Id.navigation_calendar:
                    listView.Visibility = ViewStates.Invisible;
                    return true;
                case Resource.Id.navigation_settings:
                    listView.Visibility = ViewStates.Invisible;
                    return true;
            }
            return false;
        }
    }
}

