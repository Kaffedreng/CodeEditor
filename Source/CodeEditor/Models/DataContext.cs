using System.Data.Entity;

namespace CodeEditor.Models {

    public class DataContext : DbContext {

        public DataContext() : base("Default") {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
