using CodeEditor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeEditorTests.UnitTests
{
    [TestClass]
    public class MainWindowViewModelTest
	{
        [TestMethod]
        public void OpenFileDialogTest()
        {
            var vm = new MainWindowViewModel();
            Assert.IsTrue(vm.OpenCommand.CanExecute(true));
        }

        [TestMethod]
        public void NewFileDialogTest()
        {
            var vm = new MainWindowViewModel();
            Assert.IsTrue(vm.NewCommand.CanExecute(true));
        }

        [TestMethod]
        public void CanWriteToTextboxOnWindow()
        {
            var vm = new MainWindowViewModel();
            Assert.AreEqual(true, vm.TextFile == null);
        }

        [TestMethod]
        public void SaveFileDialogTest()
        {
            var vm = new MainWindowViewModel();
            Assert.IsTrue(vm.SaveCommand.CanExecute(true));
        }

        [TestMethod]
        public void CanCloseMainWindow()
        {
            var vm = new MainWindowViewModel();
            Assert.IsTrue(vm.CloseMainWindowCommand.CanExecute(true));
        }
	}
}
