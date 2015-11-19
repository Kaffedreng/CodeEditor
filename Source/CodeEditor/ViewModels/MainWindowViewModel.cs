using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CodeEditor.Commands;
using Microsoft.Win32;

namespace CodeEditor.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Members of ICommand - Commands for functionality on the MainWindow
        public ICommand CloseMainWindowCommand
        {
            get
            {
                return new ActionCommand(o => CloseMainWindow());
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new ActionCommand(o => SaveFile());
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new ActionCommand(o => OpenFile());
            }
        }
        public ICommand NewCommand
        {
            get
            {
                return new ActionCommand(o => NewFile());
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the current textfile.
        /// By Jakob A. Nielsen
        /// </summary>
        private string _TextFile;

        public string TextFile
        {
            get { return _TextFile; }
            set
            {
                _TextFile = value;
                NotifyPropertyChanged();
            }
        }

        #region Command Methods
        /// <summary>
        /// Creates a new file as the user specifies.
        /// By Jakob A. Nielsen
        /// </summary>
        public void NewFile()
        {
            TextFile = "";

            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Create new file...";
                dlg.Filter = "Text file (*.txt)|*.txt";
                dlg.FilterIndex = 1;
                dlg.ShowDialog();

                File.WriteAllText(dlg.FileName, "");
            }
            catch
            {
                MessageBox.Show("Cancelled", "Info Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Opens a file and display it in the textbox.
        /// By Jakob A. Nielsen
        /// </summary>
        public void OpenFile()
        {
            int iLines = 0;

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open file...";
                dlg.Filter = "Text file (*.txt)|*.txt";
                dlg.FilterIndex = 1;
                dlg.ShowDialog();

                string[] lines = File.ReadAllLines(dlg.FileName);

                TextFile = "";

                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        TextFile += "\n";
                    }
                    else
                    {
                        if (iLines == lines.Length - 1)
                        {
                            TextFile += line;
                        }
                        else
                        {
                            TextFile += line + "\n";
                        }
                    }

                    iLines++;
                }
            }
            catch
            {
                MessageBox.Show("Cancelled", "Info Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Saves the current text to file of own choice.
        /// By JakobA. Nielsen
        /// </summary>
        public void SaveFile()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Save file...";
                dlg.Filter = "Text file (*.txt)|*.txt";
                dlg.FilterIndex = 1;
                dlg.ShowDialog();

                if (!String.IsNullOrEmpty(TextFile))
                {
                    foreach (string _Textline in TextFile.Split('\n'))
                    {
                        string Textline = _Textline.TrimEnd('\r');

                        File.WriteAllText(dlg.FileName, _TextFile);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cancelled", "Info Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Closes the application.
        /// By Jakob A. Nielsen
        /// </summary>
        public void CloseMainWindow()
        {
            Application.Current.Shutdown();
        }
    #endregion
    }
}
