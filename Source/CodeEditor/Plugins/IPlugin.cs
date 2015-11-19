using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor.Plugins {

    /// <summary>
    /// The IPlugin interface is used when a plugin is implemented.
    /// All plugins must conform to this interface.
    /// </summary>
    public interface IPlugin {

        /// <summary>
        /// The name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initialize the plugin.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Run the plugin.
        /// </summary>
        void Do();
    }
}
