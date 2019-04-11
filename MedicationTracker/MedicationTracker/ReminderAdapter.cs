using Android.App;
using Android.Views;
using Android.Widget;
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
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = list[position].DateTime.ToString();
            return view;
        }
    }
}