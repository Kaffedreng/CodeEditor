using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseMVVM.Data;
using EnterpriseMVVM.Windows;

namespace EnterpriseMVVM.Desktop.ViewModels {

    public class CustomerViewModel : ViewModel {

        public ICollection<Customer> Customers { get; private set; }

        public string FirstName;
        public string LastName;
        [EmailAddress]
        public string Email;

        private string customerName;

        public CustomerViewModel() {
            this.Customers = new ObservableCollection<Customer>();
        }

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

        public bool IsValid {
            get {
                return !String.IsNullOrWhiteSpace(FirstName) &&
                       !String.IsNullOrWhiteSpace(LastName) &&
                       !String.IsNullOrWhiteSpace(Email);
            }
        }

        public ActionCommand AddCustomerCommand {
            get {
                return new ActionCommand(p => AddCustomer(FirstName, LastName, Email),
                    p => IsValid);
            }
        }

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
