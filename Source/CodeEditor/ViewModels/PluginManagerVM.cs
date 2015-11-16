using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEditor.Plugins;
using CodeEditor.Views;

namespace CodeEditor.ViewModels
{
    
    internal class PluginManagerVm
    {
        private PluginInfo _childViewModel;

        public PluginManagerVm()
        {
            _childViewModel = new PluginInfo();
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
