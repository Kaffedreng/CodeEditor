using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeEditor.ViewModels;

namespace CodeEditorTests.UnitTests {

    [TestClass]
    public class PluginManagerVmTests {

        [TestMethod]
        public void CanExecuteAddNewPluginCommand()
        {
            var vm = new PluginManagerVm();
            Assert.IsTrue(vm.AddNewPluginCommand.CanExecute(true));
        }

        [TestMethod]
        public void CanExecuteRemovePluginCommand()
        {
            var vm = new PluginManagerVm();
            Assert.IsTrue(vm.RemovePluginCommand.CanExecute(true));
        }

        [TestMethod]
        public void CanExecuteOpenPluginManagerCommand()
        {
            var vm = new PluginManagerVm();
            Assert.IsTrue(vm.OpenPluginManagerCommand.CanExecute(true));
        }
    }
}
