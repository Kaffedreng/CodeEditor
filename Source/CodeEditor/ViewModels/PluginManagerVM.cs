// Made by Jakob A. Nielsen

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

    using CodeEditor.Commands;

    public class PluginManagerVm
    {
        /// <summary>
        /// An observable collection for the listview in the plugin manager.
        /// </summary>
        public ObservableCollection<IPlugin> plugins => new ObservableCollection<IPlugin>(Bootstrap.plugins.Values);

        /// <summary>
        /// Gets the Open plugin manager window command.
        /// </summary>
        public ICommand OpenPluginManagerCommand
        {
            get
            {
                return new ActionCommand(o => Show());   
            }
        }

        /// <summary>
        /// Gets the add plugin command.
        /// </summary>
        public ICommand AddNewPluginCommand
        {
            get
            {
                return new ActionCommand(o => AddPlugin());
            }
        }

        /// <summary>
        /// Get the remove plugin command.
        /// </summary> 
        public ICommand RemovePluginCommand
        {
            get
            {
                return new ActionCommand(o => RemovePlugin());
            }
        }

        /// <summary>
        /// Open the plugin manager window.
        /// </summary>
        public void Show()
        {
            PluginManager view = new PluginManager();
            view.ShowDialog();
        }
        /// <summary>
        /// User selects and removes a chosen plugin.
        /// </summary>
        public void RemovePlugin()
        {
            string distPath = Environment.CurrentDirectory + @"\Plugins\";

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Plugins (*.dll)|*.dll";
                newDll.FilterIndex = 1;
                newDll.InitialDirectory = Environment.CurrentDirectory + @"\Plugins\";
                newDll.ShowDialog();
                
                File.Delete(newDll.FileName);

                Bootstrap.plugins.Remove(newDll.FileName);

                MessageBox.Show("Plugin has been removed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// User can add a plugin.
        /// </summary>
        public void AddPlugin()
        {
            string distPath = Environment.CurrentDirectory + @"\Plugins\";

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Plugins (*.dll)|*.dll";
                newDll.FilterIndex = 1;
                newDll.ShowDialog();

                File.Move(newDll.FileName, distPath + Path.GetFileName(newDll.FileName));

                Bootstrap.Initialize();

                MessageBox.Show("Plugin has been added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
