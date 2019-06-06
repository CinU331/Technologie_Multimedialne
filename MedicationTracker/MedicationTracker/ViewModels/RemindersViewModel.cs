using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using MedicationTracker.Interfaces;
using MedicationTracker.Models;
using MedicationTracker.Services;

namespace MedicationTracker.ViewModels
{
    public class RemindersViewModel : BaseViewModel
    {
        public ObservableCollection<Reminder> Reminders { get; set; }

        public ICommand LoadRemindersCommand { get; private set; }

        public RemindersViewModel()
        {
            Title = "Reminders";
            Reminders = new ObservableCollection<Reminder>();

            MessagingCenter.Subscribe<NewReminderViewModel, Reminder>(this, "AddReminder", async (obj, reminder) =>
            {
                Reminder newReminder = reminder as Reminder;
                Reminders.Add(newReminder);
                await ReminderDataStore.AddItemAsync(newReminder);
            });

            MessagingCenter.Subscribe<ReminderDetailViewModel, Reminder>(this, "DeleteReminder", async (obj, reminder) =>
            {
                Reminder oldReminder = reminder as Reminder;
                Reminders.Remove(oldReminder);
                await ReminderDataStore.DeleteItemAsync(oldReminder.ID);
            });

            LoadRemindersCommand = new Command(async () => await ExecuteLoadRemindersCommand());

            // Refreshing time on view every 1 second
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                DateTime currentTime = DateTime.Now;

                for (int i = 0; i < Reminders.Count; i++)
                {
                    Reminders[i].RemainingTime = Reminders[i].Date - currentTime;

                    // Removing old reminders
                    if (Reminders[i].RemainingTime < -TimeSpan.FromSeconds(Settings.StaticSecondsToRemoveOldReminders))
                    {
                        Reminders.RemoveAt(i);
                    }
                }

                return true;
            });

            // Refreshing time on view every 10 second
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                bool ring = false;
                int id = 0;
                foreach (Reminder r in Reminders)
                {
                    if (r.RemainingTime < TimeSpan.FromSeconds(0))
                    {
                        id = Reminders.IndexOf(r);
                        ring = true;
                    }
                }

                if (ring)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile(Settings.StaticSelectedNotificationSound);
                    DependencyService.Get<INotify>().Notification("Przypomnienie", Reminders[id].Medicine.Name);
                }

                return true;
            });
        }

        async Task ExecuteLoadRemindersCommand()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                Reminders.Clear();

                IEnumerable<Reminder> reminders = await ReminderDataStore.GetItemsAsync(true);

                foreach (var r in reminders)
                {
                    Reminders.Add(r);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            finally { IsBusy = false; }
        }

        public IDataStore<Reminder> ReminderDataStore => DependencyService.Get<IDataStore<Reminder>>() ?? new MockReminderDataStore();
    }
}