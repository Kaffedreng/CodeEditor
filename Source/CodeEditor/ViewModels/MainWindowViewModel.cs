using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CodeEditor.Plugins;
using CodeEditor.Views;
using Microsoft.Win32;

namespace CodeEditor.ViewModels {

    class MainWindowViewModel : ViewModel
    {

        //private readonly IBootstrap bootstrap;
        //private Encoding encoding;
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
        }
        public ICommand NewCommand
        {
            get
            {
                return new ActionCommand(o => NewFile());
            }
        }
        #endregion

        private string _TextFile;

        public string TextFile {
            get { return _TextFile; }
            set
            {
                _TextFile = value;
                NotifyPropertyChanged();
            }
        }

        public void NewFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save file...";
            dlg.Filter = "Text file (*.txt)|*.txt";
            dlg.FilterIndex = 1;
            dlg.ShowDialog();

            File.WriteAllText(dlg.FileName, "");
        }

        /// <summary>
        /// Opens a file and display it in the textbox.
        /// </summary>
        public void OpenFile()
        {
            int iLines = 0;

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open file...";
                dlg.Filter = "Text file (*.txt)|*.txt";
                dlg.FilterIndex = 1;
                dlg.ShowDialog();

                string[] lines = File.ReadAllLines(dlg.FileName);

                foreach (string line in lines)
                {
                    if (line == "")
                    {
                        TextFile += "\n";
                    }
                    else
                    {
                        if (iLines == lines.Length - 1)
                        {
                            TextFile += line;
                        }
                        else
                        {
                            TextFile += line + "\n";
                        }
                    }

                    iLines++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Saves the current text to file of own choice.
        /// By JakobA. Nielsen
        /// </summary>
        public void SaveFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save file...";
            dlg.Filter = "Text file (*.txt)|*.txt";
            dlg.FilterIndex = 1;
            dlg.ShowDialog();

            if (!String.IsNullOrEmpty(TextFile))
            {
                foreach (string _Textline in TextFile.Split('\n'))
                {
                    string Textline = _Textline.TrimEnd('\r');

                    File.WriteAllText(dlg.FileName, _TextFile);
                }
            }
        }

        /// <summary>
        /// Closes the application.
        /// By Jakob
        /// </summary>
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

