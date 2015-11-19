using System.Runtime.CompilerServices;

namespace CodeEditor.Models {

    using System.ComponentModel;

    public class ObservableObject : INotifyPropertyChanged {

        /// <summary>
        /// Event used to handle property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method is called whenever the property is changed.
        /// </summary>
        /// <param name="propertyName">The property that changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {

            // Sets the PropertyChangedEventHandler variable to whatever property was changed
            PropertyChangedEventHandler handler = PropertyChanged;

            // If ´handler´ is not empty, call PropertyChangedEventArgs
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
