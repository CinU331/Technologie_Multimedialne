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

            List<Reminder> mockReminders = new List<Reminder>()
            {
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Baraberol",
                        Description = "Baraperol w syropie",
                        Image = new Image()
                        {
                            Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png")
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Raz na dzień"
                },
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                   Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Baraberol",
                        Description = "Baraperol w syropie",
                        Image = new Image()
                        {
                            Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png")
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Dwa razy na dzień"
                },
                new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Medicine = new Medicine()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = "Baraberol",
                        Description = "Baraperol w syropie",
                        Image = new Image()
                        {
                            Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png")
                        }
                    },
                    Date = DateTime.Now,
                    Portion = "Dwie porcje rano i wieczorem"
                }
            };

            foreach (Reminder r in mockReminders)
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