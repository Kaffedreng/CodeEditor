using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeEditor.Models;

namespace CodeEditorTests.FunctionalTests {

    [TestClass]
    public class CustomerScenarioTests : FunctionalTest {

        [TestMethod]
        public void AddNewCustomerIsPersisted() {

            using (var dc = new DataContext())
            using (var bc = new BusinessContext()) {

                var customer = new Customer {
                    Email = "customer@northwind.com",
                    FirstName = "David",
                    LastName = "Anderson"
                };

                bc.AddNewCustomer(customer);

                bool exists = bc.DataContext.Customers.Any(c => c.Id == customer.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}
