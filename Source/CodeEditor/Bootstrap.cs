﻿using System;
using System.Collections.Generic;
using System.Data.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEditor.Plugins;

namespace CodeEditor {

    public static class Bootstrap {

        private static Dictionary<string, IPlugin> plugins;

        public static void Initialize() {

            // Check for updates

            // Load Plug-Ins
            plugins = new Dictionary<string, IPlugin>();
            ICollection<IPlugin> loadedPlugins = PluginLoader<IPlugin>.LoadPlugins("Plugins");
            foreach (var item in loadedPlugins) {
                plugins.Add(item.Name, item);
            }

            // Initialize Plug-Ins
            foreach (var item in plugins) {
                IPlugin plugin = item.Value;
                plugin.Initialize();
                plugin.Do();
            }
        }

    }
}
