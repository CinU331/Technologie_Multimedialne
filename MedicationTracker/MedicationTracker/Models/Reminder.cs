using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedicationTracker.Models
{
    public class Reminder : INotifyPropertyChanged
    {
        public string ID { get; set; }
        public Medicine Medicine { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string Portion { get; set; }

        public TimeSpan RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                if (_remainingTime != value)
                {
                    _remainingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Data store
        private TimeSpan _remainingTime { get; set; }
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