using System.Windows;

namespace CodeEditor.Plugins.Test {
    internal class SamplePlugin2 : IPlugin {

        public string Name => "Testing";

        public void Initialize()
        {
        //    throw new NotImplementedException();
        }

        public void Do() {
            MessageBox.Show("Testing");
        }
    }
}
