using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CodeEditor.Models;

namespace CodeEditor.ViewModels {

    // TODO: Document
    public class CustomerViewModel : ViewModel {

        // TODO: Document
        public ICollection<Customer> Customers { get; private set; }

        // TODO: Document
        public string FirstName;
        public string LastName;
        [EmailAddress]
        public string Email;

        private string customerName;

        // TODO: Document
        public CustomerViewModel() {
            this.Customers = new ObservableCollection<Customer>();
        }

        // TODO: Document
        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string CustomerName {
            get {
                return customerName;
            }
            set {
                customerName = value;
                NotifyPropertyChanged();
            }
        }

        // TODO: Document
        public bool IsValid {
            get {
                return !String.IsNullOrWhiteSpace(FirstName) &&
                       !String.IsNullOrWhiteSpace(LastName) &&
                       !String.IsNullOrWhiteSpace(Email);
            }
        }

        // TODO: Document
        public ActionCommand AddCustomerCommand {
            get {
                return new ActionCommand(p => AddCustomer(FirstName, LastName, Email),
                    p => IsValid);
            }
        }

        // TODO: Document
        private void AddCustomer(string firstName, string lastName, string email) {
            using (var api = new BusinessContext()) {
                var customer = new Customer {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                try {
                    api.AddNewCustomer(customer);
                }
                catch (Exception e) {
                    // TODO: Error Handling
                    return;
                }

                Customers.Add(customer);
            }
        }
    }
}
