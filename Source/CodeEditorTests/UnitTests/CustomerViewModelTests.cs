using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseMVVM.Desktop.ViewModels;
using EnterpriseMVVM.Windows;

namespace EnterpriseMVVM.DesktopClient.Tests.UnitTests {

    [TestClass]
    public class CustomerViewModelTests {

        [TestMethod]
        public void IsViewModel() {
            Assert.IsTrue(typeof(CustomerViewModel).BaseType == typeof(ViewModel)); 
        }

        [TestMethod]
        public void ValidationErrorWhenCustomerNameExceeds32Characters() {
            var viewModel = new CustomerViewModel {
                CustomerName = "1234567890abcd efghijklmnopqrstvxyz"
            };

            Assert.IsNotNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void ValidationErrorWhenCustomerNameIsNotGreaterThanOrEqualTo2Characters() {
            var viewModel = new CustomerViewModel {
                CustomerName = "B"
            };

            Assert.IsNotNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void ValidationErrorWhenCustomerNameIsNotProvided() {
            var viewModel = new CustomerViewModel {
                CustomerName = null
            };

            Assert.IsNotNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void NoValidationErrorWhenCustomerNameMeetsAllRequirements() {
            var viewModel = new CustomerViewModel {
                CustomerName = "David Anderson"
            };

            Assert.IsNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenFirstNameIsNotValid() {
            var viewModel = new CustomerViewModel {
                FirstName = null,
                LastName = "Anderson",
                Email = "noreply@northwind.com"
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenLastNameIsNotValid() {
            var viewModel = new CustomerViewModel {
                FirstName = "David",
                LastName = null,
                Email = "noreply@northwind.com"
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandCannotExecuteWhenEmailIsNotValid() {
            var viewModel = new CustomerViewModel {
                FirstName = "David",
                LastName = "Anderson",
                Email = null
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCommandAddsCustomerToCustomersCollectionWhenExecutedSuccessfully() {
            var viewModel = new CustomerViewModel {
                FirstName = "David",
                LastName = "Anderson",
                Email = "noreply@northwind.com"
            };

            viewModel.AddCustomerCommand.Execute();

            Assert.IsTrue(viewModel.Customers.Count == 1);
        }
    }
}
