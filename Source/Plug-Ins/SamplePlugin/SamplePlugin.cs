using System.Windows;

namespace CodeEditor.Plugins {
    internal class SamplePlugin : IPlugin {

        public string Name {
            get {
                return "SamplePlugin";
            }
        }

        public void Initialize()
        {
        //    throw new NotImplementedException();
        }

        public void Do() {
            MessageBox.Show("This is a Sample Plugin.");
        }
    }
}
