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

        public ObservableCollection<Medicine> Medicines { get; set; }

        public ICommand LoadMedicinesCommand { get; private set; }
        public ICommand SaveReminderCommand { get; private set; }

        public NewReminderViewModel()
        {
            Title = "New reminder";

            /* TODO SECTION */
            NewReminder = new Reminder()
            {
                ID = Guid.NewGuid().ToString(),
                Medicine = new Medicine()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Aerozol",
                    Description = "Krystalicznie świeże powietrze"
                },
                Date = DateTime.Now,
                Portion = "A lot of pills"
            };
            /* END TODO SECTION */

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
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            finally { IsBusy = false; }
        }

        void ExecuteSaveReminderCommand()
        {
            MessagingCenter.Send(this, "AddReminder", NewReminder);
        }

        public IDataStore<Medicine> MedicineDataStore => DependencyService.Get<IDataStore<Medicine>>() ?? new MockMedicineDataStore();
    }
}