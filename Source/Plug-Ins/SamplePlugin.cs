﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//using CodeEditor.Interfaces;
//using CodeEditor.Plugins;

namespace CodeEditor.Plugins {

    class SamplePlugin : IPlugin {

        public string Name {
            get {
                return "SamplePlugin";
            }
        }

        public void Do() {
            System.Windows.MessageBox.Show("This is a Sample Plugin.");
        }
    }
}
