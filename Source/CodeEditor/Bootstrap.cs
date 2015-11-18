using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodeEditor.Plugins;

namespace CodeEditor {

    public static class Bootstrap {

        public static Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        public static void Initialize() {

            // Check for updates

            // Load Plug-Ins
            ICollection<IPlugin> loadedPlugins = PluginLoader<IPlugin>.LoadPlugins("Plugins");
            if (loadedPlugins != null)
            {
                foreach (var item in loadedPlugins)
                {
                    plugins.Add(item.Name, item);
                }

                // Initialize Plug-Ins
                foreach (var item in plugins)
                {
                    IPlugin plugin = item.Value;
                    //plugin.Initialize();
                    plugin.Do();
                }
            }
            else
            {
                MessageBox.Show("No plugins was found.");
            }
        }
    }
}
