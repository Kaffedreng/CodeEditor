using System;
using System.Collections.Generic;
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

        public PluginManagerVm()
        {
            _childViewModel = new PluginInfo();
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

            _childViewModel.Info = " was updated in the database.";

            view.ShowDialog();
        }
    }
}
