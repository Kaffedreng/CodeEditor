using System;
using System.Collections.ObjectModel;
using CodeEditor.Plugins;
using CodeEditor.Views;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace CodeEditor.ViewModels
{
    public class PluginManagerVm
    {
        private readonly PluginInfo _childViewModel;
        private ObservableCollection<IPlugin> pluginsCollection;
 
        public PluginManagerVm()
        {
            _childViewModel = new PluginInfo();
        }

        public ObservableCollection<IPlugin> plugins => new ObservableCollection<IPlugin>(Bootstrap.plugins.Values);

        /// <summary>
        /// Gets the UpdateCommand for the ViewModel
        /// </summary>
        public ICommand UpdateCommand
        {
            get
            {
                return new ActionCommand(o => Show());   
            }
        }

        public ICommand AddNewPluginCommand
        {
            get
            {
                return new ActionCommand(o => AddPlugin());
            }
        }

        public ICommand RemovePluginCommand
        {
            get
            {
                return new ActionCommand(o => RemovePlugin());
            }
        }

        public void Show()
        {
            PluginManager view = new PluginManager()
            {
                DataContext = _childViewModel
            };
            
            view.ShowDialog();
        }

        public void RemovePlugin()
        {
            string fileName = "";
            string[] temp;
            string distPath = Environment.CurrentDirectory + @"\Plugins\";

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Plugins (*.dll)|*.dll";
                newDll.FilterIndex = 1;
                newDll.InitialDirectory = Environment.CurrentDirectory + @"\Plugins\";
                newDll.ShowDialog();

                File.Delete(newDll.FileName);

                MessageBox.Show("Plugin has been removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AddPlugin()
        {
            string fileName = "";
            string distPath = Environment.CurrentDirectory + @"\Plugins\";

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Plugins (*.dll)|*.dll";
                newDll.FilterIndex = 1;

                newDll.ShowDialog();

                File.Move(newDll.FileName, distPath + Path.GetFileName(newDll.FileName));

                MessageBox.Show("Plugin has been added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
