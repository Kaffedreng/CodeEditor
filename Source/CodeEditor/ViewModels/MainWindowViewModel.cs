using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace CodeEditor.ViewModels {

    using CodeEditor.Commands;

    class MainWindowViewModel : ViewModel
    {

        //private readonly IBootstrap bootstrap;
        //private Encoding encoding;
        private string text;

        public MainWindowViewModel()
        {
        }

        #region Members of ICommand - Commands for functionality on the MainWindow
        public ICommand CloseMainWindowCommand
        {
            get
            {
                return new ActionCommand(o => CloseMainWindow());
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new ActionCommand(o => SaveFile());
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new ActionCommand(o => OpenFile());
            }
            //set { text = value; }
        }
        public ICommand NewCommand
        {
            get
            {
                return new ActionCommand(o => NewFile());
            }
        }
        #endregion

        public void NewFile()
        {

        }

        public void OpenFile()
        {
            string distPath = Environment.CurrentDirectory + @"\Plugins\";

            try
            {
                OpenFileDialog newDll = new OpenFileDialog();
                newDll.Filter = "Text file (*.txt)|*.txt";
                newDll.FilterIndex = 1;
                newDll.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveFile()
        {

        }

        public void CloseMainWindow()
        {
            Application.Current.Shutdown();
        }

        //private readonly IEventAggregator eventAggregator = new EventAggregator();

        /// <summary>
        /// Main constructor that creates a new instance of save, open and new command.
        /// </summary>
        //public EditorViewModel() {
        //    //var logProperties = new NameValueCollection();
        //    //logProperties["level"] = "ALL";
        //    //LogManager.Adapter = new EventAggregatorOutFactoryAdapter(this.eventAggregator, logProperties);

        //    var pluginFolder =
        //        FileSystem.Current.LocalStorage.CreateFolderAsync(
        //            AppDomain.CurrentDomain.BaseDirectory + @"Plugins",
        //            CreationCollisionOption.OpenIfExists);

        //    var logFolder =
        //        FileSystem.Current.LocalStorage.CreateFolderAsync(
        //            AppDomain.CurrentDomain.BaseDirectory + @"Logs",
        //            CreationCollisionOption.OpenIfExists);

        //    this.bootstrap = new SimpleBootstrap(".dll", eventAggregator);
        //    this.SaveCommand = new RelayCommand(this.SaveFile);
        //    this.OpenCommand = new RelayCommand(this.OpenFile);
        //    this.NewCommand = new RelayCommand(this.NewFile);

        //    eventAggregator.GetEvent<FolderOpenedEvent>().Publish(pluginFolder.Result);
        //    eventAggregator.GetEvent<FolderOpenedEvent>().Publish(logFolder.Result);
        //}

        //public
        //    ActionCommand
        //    OpenCommand
        //    {
        //        get;
        //        set;
        //    }
        //public
        //    ActionCommand
        //    SaveCommand
        //    {
        //        get;
        //        set;
        //    }
        //public
        //    ActionCommand
        //    NewCommand
        //    {
        //        get;
        //        set;
        //    }

            /// <summary>
            /// Gets and sets the encoding used to handle strings
            /// </summary>
            //public Encoding Encoding {
            //    get {
            //        if (this.encoding == null)
            //            this.encoding = Encoding.Default;

            //        return this.encoding;
            //    }
            //    set {
            //        this.encoding = value;
            //        this.RaisePropertyChanged("Encoding");
            //    }
            //}

            /// <summary>
            /// Reads the text to and from the textbox or sets the text equal to the encoded value and calls the RaisedPropertyChanged method.
            /// </summary>
            //public string Text {
            //    get {
            //        return this.text;
            //    }
            //    set {
            //        this.text = value;
            //        this.RaisePropertyChanged("Text");
            //    }
            //}

            /// <summary>
            /// Async task that opens the "open file" dialog and loads .txt files into the textfield
            /// </summary>
            //private async void OpenFile() {
            //    var fileDialog = new FileDialogViewModel();
            //    fileDialog.Extension = "*.txt";
            //    fileDialog.Filter = "Text documents (.txt)|*.txt";
            //    fileDialog.OpenCommand.Execute(null);

            //    using (var sr = new StreamReader(fileDialog.Stream, true)) {
            //        this.Encoding = sr.CurrentEncoding;
            //        this.Text = await sr.ReadToEndAsync();
            //    }
            //}

            /// <summary>
            /// Async task that calls FileDialogViewModel, takes the content of the textbox and excecutes the SaveCommand
            /// </summary>
            //private async void SaveFile() {
            //    var fileDialog = new FileDialogViewModel();
            //    fileDialog.Extension = "*.txt";
            //    fileDialog.Filter = "Text documents (.txt)|*.txt";
            //    fileDialog.SaveCommand.Execute(null);

            //    using (var sr = new StreamWriter(fileDialog.Stream, this.Encoding)) {
            //        await sr.WriteAsync(this.Text.ToString(CultureInfo.InvariantCulture));
            //    }
            //}

            /// <summary>
            /// Task used to clear the textbox
            /// </summary>
            //private void NewFile() {
            //    this.Text = string.Empty;
            //}
            //Console.WriteLine("Loaded MainWindowViewModel");
    }
}

