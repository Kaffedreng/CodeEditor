﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeEditorTests.UnitTests {

    using CodeEditor.Models;

    [TestClass]
    public class ObservableObjectTests {

        [TestMethod]
        public void PropertyChangedEventHandlerIsRaised() {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) => {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some Value";

            if (!raised) {
                Assert.Fail("PropertyChanged was never invoked.");
            }
        }

        class StubObservableObject : ObservableObject {

            private string changedProperty;
            public string ChangedProperty {
                get {
                    return changedProperty;
                }
                set {
                    changedProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
