using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseMVVM.Data.Tests.UnitTests {

    [TestClass]
    public class FunctionalTest {

        [TestInitialize]
        public virtual void TestInitialize() {
            using (var db = new DataContext()) {
                if (db.Database.Exists()) {
                    db.Database.Delete();
                }

                db.Database.Create();
            }
        }

        [TestCleanup]
        public virtual void TestCleanup() {
            using (var db = new DataContext()) {
                if (db.Database.Exists()) {
                    db.Database.Delete();
                }
            }
        }
    }
}
