using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseMVVM.Data.Tests.FunctionalTests {

    [TestClass]
    public class DatabaseScenarioTests {

        [TestMethod]
        public void CanCreateDatabase() {
            using (var db = new DataContext()) {
                db.Database.Create();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup() {
            using (var db = new DataContext()) {
                if (db.Database.Exists()) {
                    db.Database.Delete();
                }
            }
        }
    }
}
