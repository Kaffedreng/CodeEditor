using System.Runtime.CompilerServices;

namespace CodeEditor.ViewModels {

    using System.ComponentModel;

    // TODO: Document
    public class ObservableObject : INotifyPropertyChanged {

        // TODO: Document
        public event PropertyChangedEventHandler PropertyChanged;

        // TODO: Document
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
