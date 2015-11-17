using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodeEditor.Plugins;

namespace CodeEditor {

    public static class Bootstrap {

        public static Dictionary<string, IPlugin> plugins;

        public static void Initialize() {

            // Check for updates

            // Load Plug-Ins
            plugins = new Dictionary<string, IPlugin>();

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
