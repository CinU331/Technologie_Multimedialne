using Android.App;
using Android.Graphics;
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
        bool isOnConstructor = true;
        ListView listView;
        List<Reminder> mlist;
        ReminderAdapter adapter;
        Button timePicker;
        Button datePicker;
        TextView timeTextView;
        TextView dateTextView;
        CheckBox checkBox;
        TextView medicineNameTextView;
        EditText medicineName;
        Button generateReminder;
        DateTime globalDate;
        DateTime globalTime;
        Spinner medicamentSpinner;
        ArrayAdapter medicamentAdapter;
        TextView optionalTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            mlist = new List<Reminder>
            {
                new Reminder
                {
                    Medicine = "Apap",
                    Date = DateTime.Parse("2019-03-21"),
                    Time = DateTime.Parse("02:25")
                }
            };
            listView = FindViewById<ListView>(Resource.Id.listView1);
            adapter = new ReminderAdapter(this, mlist);
            listView.Adapter = adapter;
            listView.ItemClick += ListView_ItemClick;

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            timePicker = FindViewById<Button>(Resource.Id.button1);
            timePicker.Click += TimeSelectOnClick;
            timeTextView = FindViewById<TextView>(Resource.Id.textView1);

            datePicker = FindViewById<Button>(Resource.Id.button2);
            datePicker.Click += DateSelectOnClick;
            dateTextView = FindViewById<TextView>(Resource.Id.textView2);

            medicineNameTextView = FindViewById<TextView>(Resource.Id.textView3);
            medicineName = FindViewById<EditText>(Resource.Id.editText1);

            generateReminder = FindViewById<Button>(Resource.Id.button3);
            generateReminder.Click += GenerateClicked;

            checkBox = FindViewById<CheckBox>(Resource.Id.checkBox1);
            var layout = FindViewById<RelativeLayout>(Resource.Id.container);
            checkBox.Click += (o, e) => {
                if (checkBox.Checked)
                    layout.SetBackgroundColor(Color.LightSeaGreen);
                else
                    layout.SetBackgroundColor(Color.White);
            };
            medicamentSpinner = FindViewById<Spinner>(Resource.Id.spinner1);
            medicamentAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ListOfMedicaments.Medicaments);
            medicamentSpinner.Adapter = medicamentAdapter;
            medicamentSpinner.ItemSelected += MedicamentSpinner_ItemSelected;
            optionalTextView = FindViewById<TextView>(Resource.Id.textView4);


            listView.Visibility = ViewStates.Visible;
            timePicker.Visibility = ViewStates.Invisible;
            timeTextView.Visibility = ViewStates.Invisible;
            datePicker.Visibility = ViewStates.Invisible;
            dateTextView.Visibility = ViewStates.Invisible;
            checkBox.Visibility = ViewStates.Invisible;
            medicineName.Visibility = ViewStates.Invisible;
            medicineNameTextView.Visibility = ViewStates.Invisible;
            generateReminder.Visibility = ViewStates.Invisible;
            medicamentSpinner.Visibility = ViewStates.Invisible;
            optionalTextView.Visibility = ViewStates.Invisible;
        }

        private void MedicamentSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(!isOnConstructor)
            {
                Toast.MakeText(this, ListOfMedicaments.Medicaments[e.Position], ToastLength.Short).Show();
            }
        }

        private void GenerateClicked(object sender, EventArgs e)
        {
            if (timeTextView != null && dateTextView != null)
            {
                if (medicineNameTextView != null && medicineName.Text != "")
                {
                    mlist.Add(new Reminder
                    {
                        Medicine = medicineName.Text,
                        Date = globalDate,
                        Time = globalTime
                    });
                }
                else
                {
                    mlist.Add(new Reminder
                    {
                        Medicine = ListOfMedicaments.Medicaments[medicamentSpinner.SelectedItemPosition],
                        Date = globalDate,
                        Time = globalTime
                    });
                }
            }
        }

        private void DateSelectOnClick(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(
                delegate (DateTime date)
                {
                    globalDate = date;
                    dateTextView.Text = date.ToString("dd-MM-yyyy");
                });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        void TimeSelectOnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
                delegate (DateTime time)
                {
                    globalTime = time;
                    timeTextView.Text = time.ToString("HH:mm");
                });

            frag.Show(FragmentManager, TimePickerFragment.TAG);
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
                    timePicker.Visibility = ViewStates.Invisible;
                    timeTextView.Visibility = ViewStates.Invisible;
                    datePicker.Visibility = ViewStates.Invisible;
                    dateTextView.Visibility = ViewStates.Invisible;
                    checkBox.Visibility = ViewStates.Invisible;
                    medicineName.Visibility = ViewStates.Invisible;
                    medicineNameTextView.Visibility = ViewStates.Invisible;
                    generateReminder.Visibility = ViewStates.Invisible;
                    medicamentSpinner.Visibility = ViewStates.Invisible;
                    optionalTextView.Visibility = ViewStates.Invisible;
                    return true;
                case Resource.Id.navigation_calendar:
                    isOnConstructor = false;
                    listView.Visibility = ViewStates.Invisible;
                    checkBox.Visibility = ViewStates.Invisible;
                    timePicker.Visibility = ViewStates.Visible;
                    timeTextView.Visibility = ViewStates.Visible;
                    datePicker.Visibility = ViewStates.Visible;
                    dateTextView.Visibility = ViewStates.Visible;
                    medicineName.Visibility = ViewStates.Visible;
                    medicineNameTextView.Visibility = ViewStates.Visible;
                    generateReminder.Visibility = ViewStates.Visible;
                    medicamentSpinner.Visibility = ViewStates.Visible;
                    optionalTextView.Visibility = ViewStates.Visible;
                    return true;
                case Resource.Id.navigation_settings:
                    checkBox.Visibility = ViewStates.Visible;
                    listView.Visibility = ViewStates.Invisible;
                    timePicker.Visibility = ViewStates.Invisible;
                    timeTextView.Visibility = ViewStates.Invisible;
                    datePicker.Visibility = ViewStates.Invisible;
                    dateTextView.Visibility = ViewStates.Invisible;
                    medicineName.Visibility = ViewStates.Invisible;
                    medicineNameTextView.Visibility = ViewStates.Invisible;
                    generateReminder.Visibility = ViewStates.Invisible;
                    medicamentSpinner.Visibility = ViewStates.Invisible;
                    optionalTextView.Visibility = ViewStates.Invisible;
                    return true;
            }
            return false;
        }
    }
}

