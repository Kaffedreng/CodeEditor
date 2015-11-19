using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace CodeEditor.ViewModels {

    using CodeEditor.Commands;

    class MainWindowViewModel : ViewModel
    {
        private string text;

        public MainWindowViewModel()
        {
        }

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

        public void NewFile()
        {

            }

        public void OpenFile()
        {
            int iLines = 0;

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Text file (*.txt)|*.txt";
                newDll.FilterIndex = 1;
                newDll.ShowDialog();
                    }
            catch (Exception ex)
                    {
                MessageBox.Show(ex.ToString());
                        }
                        }

        public void SaveFile()
        {

                    }

        public void CloseMainWindow()
        {
            Application.Current.Shutdown();
        }
    }
}

