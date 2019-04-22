using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using MedicationTracker.Models;

namespace MedicationTracker.Services
{
    public class MockReminderDataStore : IDataStore<Reminder>
    {
        List<Reminder> reminders;

        public MockReminderDataStore()
        {
            reminders = new List<Reminder>();
            var mockReminders = new List<Reminder>
            {
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Kolanozol",
                        Description = "Na kolana",
                        Image = new Image()
                        {
                            Source = "https://cdn.pixabay.com/photo/2018/06/29/08/15/doodle-3505459_960_720.png"
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Twice times a day"
                },
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Kolanozol",
                        Description = "Na kolana",
                        Image = new Image()
                        {
                            Source = "https://cdn.pixabay.com/photo/2018/06/29/08/15/doodle-3505459_960_720.png"
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Twice times a day"
                },
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Kolanozol",
                        Description = "Na kolana",
                        Image = new Image()
                        {
                            Source = "https://cdn.pixabay.com/photo/2018/06/29/08/15/doodle-3505459_960_720.png"
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Twice times a day"
                }
            };

            foreach (var r in mockReminders)
            {
                reminders.Add(r);
            }
        }

        public async Task<bool> AddItemAsync(Reminder reminder)
        {
            reminders.Add(reminder);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Reminder reminder)
        {
            var oldReminder = reminders.Where((Reminder arg) => arg.ID == reminder.ID).FirstOrDefault();
            reminders.Remove(oldReminder);
            reminders.Add(reminder);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldReminder = reminders.Where((Reminder arg) => arg.ID == id).FirstOrDefault();
            reminders.Remove(oldReminder);

            return await Task.FromResult(true);
        }

        public async Task<Reminder> GetItemAsync(string id)
        {
            return await Task.FromResult(reminders.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Reminder>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(reminders);
        }
    }
}