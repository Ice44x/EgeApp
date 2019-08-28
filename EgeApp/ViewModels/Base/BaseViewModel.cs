using PropertyChanged;
using System.ComponentModel;

namespace EgeApp
{
    /// <summary>
    /// A base view model  that fires  Property Changed events as needed
    /// </summary>
    
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired  when any child property is changed its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}