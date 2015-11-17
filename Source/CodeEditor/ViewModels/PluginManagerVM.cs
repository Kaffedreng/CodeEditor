using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEditor.Plugins;
using CodeEditor.Views;
using System.Windows.Input;

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

        public ObservableCollection<IPlugin> plugins
        {
            get
            {
                return new ObservableCollection<IPlugin>(Bootstrap.plugins.Values);
            }
        }

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

        public void Show()
        {
            PluginManager view = new PluginManager()
            {
                DataContext = _childViewModel
            };

            
            view.ShowDialog();
        }
    }
}
