using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using MedicationTracker.Models;
using MedicationTracker.Services;
using MedicationTracker.Views;

namespace MedicationTracker.ViewModels
{
    public class NewReminderViewModel : BaseViewModel
    {
        public Reminder NewReminder { get; set; }
        public List<TimeSpan> TimeSpans = new List<TimeSpan> { new TimeSpan(0, 0, 0), new TimeSpan(0, 1, 0), new TimeSpan(6, 0, 0), new TimeSpan(12, 0, 0), new TimeSpan(24, 0, 0) };

        public Medicine SelectedMedicine
        {
            get { return _medicine; }
            set
            {
                _medicine = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedTime
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Medicine> Medicines { get; set; }

        public ICommand LoadMedicinesCommand { get; private set; }
        public ICommand SaveReminderCommand { get; private set; }

        public NewReminderViewModel()
        {
            Title = "New reminder";

            // Create empty medicine
            _customMedicine = new Medicine()
            {
                ID = Guid.NewGuid().ToString(),
                Name = "",
                Description = ""
            };

            // Create empty reminder
            NewReminder = new Reminder()
            {
                ID = Guid.NewGuid().ToString(),
                Medicine = _customMedicine,
                Date = DateTime.Now,
                Portion = "Brak",
                TimeSpan = new TimeSpan(0,0,0)
            };

            Medicines = new ObservableCollection<Medicine>();

            MessagingCenter.Subscribe<NewReminderPage, Medicine>(this, "AddMedicine", async (obj, medicine) =>
            {
                Medicine newMedicine = medicine as Medicine;
                Medicines.Add(newMedicine);
                await MedicineDataStore.AddItemAsync(newMedicine);
            });

            LoadMedicinesCommand = new Command(async () => await ExecuteLoadMedicinesCommand());
            SaveReminderCommand = new Command(() => ExecuteSaveReminderCommand());
        }

        async Task ExecuteLoadMedicinesCommand()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                Medicines.Clear();

                IEnumerable<Medicine> medicines = await MedicineDataStore.GetItemsAsync(true);

                foreach (var m in medicines)
                {
                    Medicines.Add(m);
                }
                Medicines.Add(new Medicine
                {
                    ID = "Własny",
                    Name = "Twój własny lek",
                    Description = "Kliknij aby go zmodyfikować",
                    Image = new Image { Source = ImageSource.FromResource("MedicationTracker.Resources.pills.png") }
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            finally { IsBusy = false; }
        }

        void ExecuteSaveReminderCommand()
        {
            if (SelectedMedicine != null)
            {
                NewReminder.Medicine = SelectedMedicine;
            }

            NewReminder.Date = new DateTime(
                SelectedDate.Year,
                SelectedDate.Month,
                SelectedDate.Day,
                SelectedTime.Hours,
                SelectedTime.Minutes,
                SelectedTime.Seconds);
            NewReminder.RemainingTime = NewReminder.Date - DateTime.Now;

            MessagingCenter.Send(this, "AddReminder", NewReminder);
        }

        public IDataStore<Medicine> MedicineDataStore => DependencyService.Get<IDataStore<Medicine>>() ?? new MockMedicineDataStore();

        #region Data store
        private Medicine _medicine { get; set; } = null;
        private Medicine _customMedicine { get; set; } = null;
        private DateTime _date { get; set; } = DateTime.Now;
        private TimeSpan _time { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        #endregion
    }
}