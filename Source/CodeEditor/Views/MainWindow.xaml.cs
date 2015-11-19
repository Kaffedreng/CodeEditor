using System.Windows;

namespace CodeEditor.Views {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly TextEditorControl _textEditorControl;

        public MainWindow() {
            InitializeComponent();

            _textEditorControl = new TextEditorControl();

            // Initialize Plugins, etc.
            Bootstrap.Initialize();
        }

        private void EditorTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (_textEditorControl != null) {
                _textEditorControl.TextDidChange(sender, e);
            }
        }
    }
}
