using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseMVVM.Data.Tests.UnitTests {

    [TestClass]
    public class BusinessContextTests {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsExeption_WhenEmailIsNull() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = null,
                    FirstName = "David",
                    LastName = "Anderson"
                };

                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsExeption_WhenEmailIsEmpty() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = "",
                    FirstName = "David",
                    LastName = "Anderson"
                };

                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsExeption_WhenFirstNameIsNull() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = "customer@northwind.com",
                    FirstName = null,
                    LastName = "Anderson"
                };

                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsExeption_WhenFirstNameIsEmpty() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = "customer@northwind.com",
                    FirstName = "",
                    LastName = "Anderson"
                };

                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsExeption_WhenLastNameIsNull() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = "customer@northwind.com",
                    FirstName = "David",
                    LastName = null
                };

                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsExeption_WhenLastNameIsEmpty() {
            using (var bc = new BusinessContext()) {
                var customer = new Customer {
                    Email = "customer@northwind.com",
                    FirstName = "David",
                    LastName = ""
                };

                bc.AddNewCustomer(customer);
            }
        }
    }
}
