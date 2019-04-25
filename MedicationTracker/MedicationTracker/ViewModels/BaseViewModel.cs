using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MedicationTracker.Models;
using MedicationTracker.Services;

namespace MedicationTracker.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Medicine> MedicineDataStore => DependencyService.Get<IDataStore<Medicine>>() ?? new MockMedicineDataStore();
        public IDataStore<Reminder> ReminderDataStore => DependencyService.Get<IDataStore<Reminder>>() ?? new MockReminderDataStore();
        
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string name = null, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) { return false; }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(name);

            return true;
        }

        #region Data store
        private bool _isBusy = false;
        private string _title = string.Empty;
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}