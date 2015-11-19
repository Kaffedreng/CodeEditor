using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
