using System.Collections.Generic;
using System.Windows;
using CodeEditor.Plugins;

namespace CodeEditor {

    public static class Bootstrap {

        public static Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        /// <summary>
        /// Collects plugins, if there is any, and initializes them.
        /// </summary>
        public static void Initialize() {

            // Check for updates

            // Load Plug-Ins
            var loadedPlugins = PluginLoader<IPlugin>.LoadPlugins("Plugins");
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
                    plugin.Initialize();
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
