using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor.Plugins {

    public interface IPlugin {

        string Name { get; }

        void Initialize();

        void Do();
    }
}
