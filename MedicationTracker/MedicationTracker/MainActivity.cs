using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlertDialog = Android.App.AlertDialog;

namespace MedicationTracker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        bool isOnConstructor = true;
        ListView listView;
        List<Reminder> remainders;
        ReminderAdapter adapter;
        Button timePicker;
        Button datePicker;
        TextView timeTextView;
        TextView dateTextView;
        TextView portionIntervalText;
        CheckBox checkBox;
        EditText medicineName;
        EditText portionDescription;
        Button generateReminder;
        DateTime globalDate;
        DateTime globalTime;
        Spinner medicamentSpinner;
        Spinner intervalSpinner;
        ArrayAdapter medicamentAdapter;

        TextView spinnerStyle;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            remainders = new List<Reminder>
            {
                new Reminder
                {
                    Medicine = "Apap",
                    Date = DateTime.Now,
                    Time = DateTime.Now,
                    Portion = "1 cała tabletka",
                    Interval = 6
                }
            };
            listView = FindViewById<ListView>(Resource.Id.listView1);
            adapter = new ReminderAdapter(this, remainders);
            listView.Adapter = adapter;
            listView.ItemLongClick += ListView_ItemLongClick;

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            timePicker = FindViewById<Button>(Resource.Id.button1);
            timePicker.Click += TimeSelectOnClick;

            timeTextView = FindViewById<TextView>(Resource.Id.textView1);
            timeTextView.AfterTextChanged += TimeTextView_AfterTextChanged;

            datePicker = FindViewById<Button>(Resource.Id.button2);
            datePicker.Click += DateSelectOnClick;

            dateTextView = FindViewById<TextView>(Resource.Id.textView2);
            dateTextView.AfterTextChanged += DateTextView_AfterTextChanged;

            medicineName = FindViewById<EditText>(Resource.Id.editText1);
            medicineName.AfterTextChanged += MedicineName_AfterTextChanged;

            portionDescription = FindViewById<EditText>(Resource.Id.editText2);
            portionDescription.AfterTextChanged += PortionDescription_AfterTextChanged1;

            portionIntervalText = FindViewById<TextView>(Resource.Id.textView3);
            intervalSpinner = FindViewById<Spinner>(Resource.Id.spinner2);
            var intervalsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Intervals, Android.Resource.Layout.SimpleListItem1);
            intervalSpinner.Adapter = intervalsAdapter;

            generateReminder = FindViewById<Button>(Resource.Id.button3);
            generateReminder.Click += GenerateClicked;

            checkBox = FindViewById<CheckBox>(Resource.Id.checkBox1);
            var layout = FindViewById<RelativeLayout>(Resource.Id.container);
            layout.SetBackgroundColor(Color.ParseColor("#8be88b"));
            navigation.SetBackgroundColor(Color.ParseColor("#4b634b"));
            checkBox.Click += (o, e) => {
                if (checkBox.Checked)
                {
                    layout.SetBackgroundColor(Color.ParseColor("#38393a"));
                    navigation.SetBackgroundColor(Color.ParseColor("#445484"));
                }
                else
                {
                    layout.SetBackgroundColor(Color.ParseColor("#8be88b"));
                    navigation.SetBackgroundColor(Color.ParseColor("#4b634b"));
                }
            };
            spinnerStyle = new TextView(this);
            spinnerStyle.SetTextColor(Color.Red);
            medicamentSpinner = FindViewById<Spinner>(Resource.Id.spinner1);
            medicamentAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ListOfMedicaments.Medicaments);
            medicamentSpinner.Adapter = medicamentAdapter;
            medicamentSpinner.ItemSelected += MedicamentSpinner_ItemSelected;


            listView.Visibility = ViewStates.Visible;
            timePicker.Visibility = ViewStates.Invisible;
            timeTextView.Visibility = ViewStates.Invisible;
            datePicker.Visibility = ViewStates.Invisible;
            dateTextView.Visibility = ViewStates.Invisible;
            checkBox.Visibility = ViewStates.Invisible;
            medicineName.Visibility = ViewStates.Invisible;
            portionDescription.Visibility = ViewStates.Invisible;
            generateReminder.Visibility = ViewStates.Invisible;
            medicamentSpinner.Visibility = ViewStates.Invisible;
            intervalSpinner.Visibility = ViewStates.Invisible;
            portionIntervalText.Visibility = ViewStates.Invisible;

            generateReminder.Enabled = false;
        }

        private void DateTextView_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (dateTextView.Text.Length == 0)
            {
                generateReminder.Enabled = false;
            }
            else if(timeTextView.Text.Length != 0 && portionDescription.Text.Length != 0)
            {
                generateReminder.Enabled = true;
            }
        }

        private void TimeTextView_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (timeTextView.Text.Length == 0)
            {
                generateReminder.Enabled = false;
            }
            else if(dateTextView.Text.Length != 0 && portionDescription.Text.Length != 0)
            {
                generateReminder.Enabled = true;
            }
        }

        private void PortionDescription_AfterTextChanged1(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if(portionDescription.Text.Length == 0)
            {
                generateReminder.Enabled = false;
            }
            else if(timeTextView.Text.Length != 0 && dateTextView.Text.Length != 0)
            {
                generateReminder.Enabled = true;
            }
        }

        private void MedicineName_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if(medicineName.Text.Length != 0)
            {
                medicamentSpinner.Enabled = false;
            }
            else
            {
                medicamentSpinner.Enabled = true;
            }
        }

        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            PopupMenu popupMenu = new PopupMenu(this, (View)sender);
            popupMenu.MenuInflater.Inflate(Resource.Menu.EventPopUpMenu, popupMenu.Menu);

            popupMenu.MenuItemClick += (s, arg) =>
            {
                if(arg.Item.TitleFormatted.ToString() == "Usuń")
                {
                    
                    remainders.RemoveAt(e.Position);
                    adapter.NotifyDataSetChanged();
                }
                if (arg.Item.TitleFormatted.ToString() == "Szczegóły")
                {
                    var builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Szczegóły");
                    builder.SetMessage("Pierwsze przyjęcie:"
                                     + "\nData: " + remainders[e.Position].Date.ToString("yyyy-MM-dd")
                                     + "\nGodzina: " + remainders[e.Position].Time.ToString("HH:mm")
                                     + "\n\nPorcja: " + remainders[e.Position].Portion
                                     + "\nOdstęp[godz]: " + remainders[e.Position].Interval);
                    builder.Show();
                }
                if (arg.Item.TitleFormatted.ToString() == "Edycja")
                {


                }
            };
            popupMenu.Show();
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
                if (medicineName.Text != "")
                {
                    remainders.Add(new Reminder
                    {
                        Medicine = medicineName.Text,
                        Date = globalDate,
                        Time = globalTime,
                        Portion = portionDescription.Text,
                        Interval = int.Parse((string)intervalSpinner.SelectedItem)
                    });
                }
                else
                {
                    remainders.Add(new Reminder
                    {
                        Medicine = ListOfMedicaments.Medicaments[medicamentSpinner.SelectedItemPosition],
                        Date = globalDate,
                        Time = globalTime,
                        Portion = portionDescription.Text,
                        Interval = int.Parse((string)intervalSpinner.SelectedItem)
                    });
                }
                adapter.NotifyDataSetChanged();
                var toast = Toast.MakeText(this, "Przypomnienie dodane pomyślnie", ToastLength.Long);
                timeTextView.Text = "";
                dateTextView.Text = "";
                portionDescription.Text = "";
                medicineName.Text = "";
                generateReminder.Enabled = false;

                toast.Show();
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
                    generateReminder.Visibility = ViewStates.Invisible;
                    medicamentSpinner.Visibility = ViewStates.Invisible;
                    portionDescription.Visibility = ViewStates.Invisible;
                    intervalSpinner.Visibility = ViewStates.Invisible;
                    portionIntervalText.Visibility = ViewStates.Invisible;
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
                    generateReminder.Visibility = ViewStates.Visible;
                    medicamentSpinner.Visibility = ViewStates.Visible;
                    portionDescription.Visibility = ViewStates.Visible;
                    intervalSpinner.Visibility = ViewStates.Visible;
                    portionIntervalText.Visibility = ViewStates.Visible;
                    return true;
                case Resource.Id.navigation_settings:
                    checkBox.Visibility = ViewStates.Visible;
                    listView.Visibility = ViewStates.Invisible;
                    timePicker.Visibility = ViewStates.Invisible;
                    timeTextView.Visibility = ViewStates.Invisible;
                    datePicker.Visibility = ViewStates.Invisible;
                    dateTextView.Visibility = ViewStates.Invisible;
                    medicineName.Visibility = ViewStates.Invisible;
                    generateReminder.Visibility = ViewStates.Invisible;
                    medicamentSpinner.Visibility = ViewStates.Invisible;
                    portionDescription.Visibility = ViewStates.Invisible;
                    intervalSpinner.Visibility = ViewStates.Invisible;
                    portionIntervalText.Visibility = ViewStates.Invisible;
                    return true;
            }
            return false;
        }
    }
}

