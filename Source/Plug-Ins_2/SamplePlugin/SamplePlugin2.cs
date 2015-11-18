using System.Windows;

namespace CodeEditor.Plugins.Test {
    internal class SamplePlugin2 : IPlugin {

        public string Name => "Testing";

        public void Initialize()
        {

        }

        public void Do() {
            MessageBox.Show("Testing");
        }
    }
}
