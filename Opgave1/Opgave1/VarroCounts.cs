using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Media;
using System.Windows.Data;
using MvvmFoundation.Wpf;

namespace Opgave1
{
    // 
    public class VarroCounts : ObservableCollection<VarroCount>, INotifyPropertyChanged
    {
        string _filename = "";

        public VarroCounts()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                // In Design mode
                Add(new VarroCount("Klosterheden", DateTime.Today, 5000, "Dejligt vejr i dag"));
                Add(new VarroCount("Risskov", DateTime.Now.AddDays(-3), 2500, "Bierne havde det godt"));
                Add(new VarroCount("Arhus", DateTime.Now.AddMonths(-3).AddDays(-1), 2500, "Bierne havde det godt"));
            }
        }

        #region Commands

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddVarro)); }
        }

        private void AddVarro()
        {
            // Show Modal Dialog
            var dlg = new VarroWindow();
            dlg.Title = "Add New VarroCount";
            VarroCount newVarro = new VarroCount();
            dlg.DataContext = newVarro;
            if (dlg.ShowDialog() == true)
            {
                Add(newVarro);
                CurrentVarro = newVarro;
            }
        }

        ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand(EditVarro, DeleteVarro_CanExecute)); }
        }

        private void EditVarro()
        {
            // Show Modal Dialog
            var dlg = new VarroWindow();
            dlg.Title = "Edit VarroCount";
            // Need a temp varro in case of cancel
            VarroCount tempVarro = new VarroCount
            {
                Bistade = CurrentVarro.Bistade,
                Time = CurrentVarro.Time,
                Count = CurrentVarro.Count,
                Comment = CurrentVarro.Comment
            };
            dlg.DataContext = tempVarro;
            if (dlg.ShowDialog() == true)
            {
                CurrentVarro.Bistade = tempVarro.Bistade;
                CurrentVarro.Time = tempVarro.Time;
                CurrentVarro.Count = tempVarro.Count;
                CurrentVarro.Comment = tempVarro.Comment;
            }
        }

        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteVarro, DeleteVarro_CanExecute)); }
        }

        private void DeleteVarro()
        {
            MessageBoxResult res = MessageBox.Show("Er du sikker på, at du vil slette denne tælling?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Remove(CurrentVarro);
                NotifyPropertyChanged("Count");
            }
        }

        private bool DeleteVarro_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
                           () => ++CurrentIndex,
                           () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _previusCommand;
        public ICommand PreviusCommand
        {
            get { return _previusCommand ?? (_previusCommand = new RelayCommand(PreviusCommandExecute, PreviusCommandCanExecute)); }
        }

        private void PreviusCommandExecute()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        private bool PreviusCommandCanExecute()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {
                MessageBox.Show("Der skal indtastes et filnavn, for at kunne gemme filen", "Kan ikke gemme fil",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                _filename = argFilename;
                SaveFileCommand_Execute();
            }
        }

        ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)); }
        }

        private void SaveFileCommand_Execute()
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(VarroCounts));
            TextWriter writer = new StreamWriter(_filename);
            // Serialize all the VarroCounts.
            serializer.Serialize(writer, this);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (_filename != "") && (Count > 0);
        }

        ICommand _newFileCommand;
        public ICommand NewFileCommand
        {
            get { return _newFileCommand ?? (_newFileCommand = new RelayCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Alt data, som ikke er gemt, vil blive slettet. Fortsæt?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Clear();
                _filename = "";
            }
        }


        ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _openFileCommand ?? (_openFileCommand = new RelayCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("Der skal indtastes et filnavn, for at kunne åbne filen", "Kan ikke åbne fil",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                _filename = argFilename;
                VarroCounts tempVarros = new VarroCounts();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(VarroCounts));
                try
                {
                    TextReader reader = new StreamReader(_filename);
                    // Deserialize all the VarroCounts.
                    tempVarros = (VarroCounts)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Kan ikke åbne fil", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // We have to insert the VarroCounts in the existing collection. 
                Clear();
                foreach (var varro in tempVarros)
                    Add(varro);
            }
        }

        ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _closeAppCommand ?? (_closeAppCommand = new RelayCommand(CloseCommand_Execute)); }
        }

        private void CloseCommand_Execute()
        {
            Application.Current.MainWindow.Close();
        }

        ICommand _ColorCommand;
        public ICommand ColorCommand
        {
            get { return _ColorCommand ?? (_ColorCommand = new RelayCommand<String>(ColorCommand_Execute)); }
        }

        private void ColorCommand_Execute(String colorStr)
        {
            SolidColorBrush newBrush = SystemColors.WindowBrush; // Default color

            try
            {
                if (colorStr != null)
                {
                    if (colorStr != "Default")
                        newBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorStr));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown color name, default color is used", "Program error!");
            }

            Application.Current.MainWindow.Resources["BackgroundBrush"] = newBrush;
        }
        #endregion // Commands

        #region Properties

        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private VarroCount _currentVarro = null;

        public VarroCount CurrentVarro
        {
            get { return _currentVarro; }
            set
            {
                if (_currentVarro != value)
                {
                    _currentVarro = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion
        
        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
