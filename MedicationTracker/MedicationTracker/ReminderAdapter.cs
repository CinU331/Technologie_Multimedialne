using Android.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace MedicationTracker
{
    public class ReminderAdapter : BaseAdapter <Reminder>
    {
        public List<Reminder> list;
        private Activity context;
        public ReminderAdapter(Activity context, List<Reminder> list) : base()
        {
            this.list = list;
            this.context = context;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Reminder this[int position]
        {
            get { return list[position]; }
        }
        public override int Count
        {
            get { return list.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            DateTime currentTime = DateTime.Now;
            DateTime initTime = new DateTime(list[position].Date.Year, list[position].Date.Month, list[position].Date.Day,
                                             list[position].Time.Hour, list[position].Time.Minute, list[position].Time.Second);
            TextView tmp = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            TimeSpan diffrence;
            string text = tmp.Text;
            while (initTime < currentTime)
            {
                initTime = initTime.AddHours(list[position].Interval);
            }
            diffrence = initTime - currentTime;
            
            tmp.SetText(list[position].Medicine + "\n" + "Pozostało " + diffrence.Hours + "g " + diffrence.Minutes + "m", TextView.BufferType.Normal);
            tmp.TextAlignment = TextAlignment.Center;
            text = tmp.Text;
            return view;
        }
    }
}