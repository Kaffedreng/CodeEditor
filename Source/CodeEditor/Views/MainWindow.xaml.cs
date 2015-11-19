using System.Windows;

namespace CodeEditor.Views {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            Bootstrap.Initialize();
        }
    }
}
