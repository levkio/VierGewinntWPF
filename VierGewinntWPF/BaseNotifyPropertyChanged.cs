using System.ComponentModel;

namespace VierGewinntWPF
{
    abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		// test commit
        /// <summary>
        /// Fire this event when a Property has changed
        /// Notify all obj that are listining
        /// </summary>
        /// <param name="propertyName"></param>
        protected void FirePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
