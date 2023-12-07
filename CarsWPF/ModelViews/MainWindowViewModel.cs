using CarsWPF.Infrastructure;
using CarsWPF.Models;
using CarsWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace CarsWPF.ModelViews
{
    internal class MainWindowViewModel: INotifyPropertyChanged
    {
        Car? selectedCar;
        public ObservableCollection<Car>? Cars { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                if(selectedCar != value) 
                {
                    selectedCar = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            /*Cars = new ObservableCollection<Car>() {
                new Car(){ Brand = "BMW", Color = "Red", MaxSpeed = 300, Image= Directory.GetCurrentDirectory() + "/Image/BMW.jpg" },
                new Car(){ Brand = "Audi", Color = "Black", MaxSpeed = 200, Image = Directory.GetCurrentDirectory() + "/Image/Audi.jpg" },
                new Car(){ Brand = "Tesla", Color = "Green", MaxSpeed = 250, Image = Directory.GetCurrentDirectory() + "/Image/Tesla.jpg" },
            };*/
            Cars = new ObservableCollection<Car>();
            if (File.Exists("Cars.xml"))
                DeserializeXML();
        }

        ICommand? deleteCommand;
        ICommand? addCommand;
        ICommand? editCommand;
        ICommand? saveCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(DelCarMethod));
            }
        }

        void DelCarMethod(object param)
        {
            if(param is Car car) 
            {
                Cars?.Remove(car);
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(AddCarMethod));
            }
        }

        void AddCarMethod(object param)
        {
            var win = new AddCar((param as MainWindow));
            win.Owner = param as MainWindow;
            win.Show();

        }

        public ICommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand(EditCarMethod, CanEditCarMethod));
            }
        }

        void EditCarMethod(object param)
        {
            var win = new EditCar((param as MainWindow));
            win.Owner = param as MainWindow;
            win.Show();
        }

        bool CanEditCarMethod(object param)
        {
            return SelectedCar != null;
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(SaveCarMethod, CanSaveCarMethod));
            }
        }

        void SaveCarMethod(object param)
        {
            SerializeXML();
            MessageBox.Show("Your settings have been saved");
        }

        bool CanSaveCarMethod(object param)
        {
            return Cars?.Count > 0;
        }

        void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        void SerializeXML()
        {
            var xml = new XmlSerializer(typeof(ObservableCollection<Car>));

            File.Delete("Cars.xml");
            using(var file = new FileStream("Cars.xml",FileMode.OpenOrCreate))
            {
                if(Cars?.Count > 0)
                    xml.Serialize(file, Cars);
            }
        }

        void DeserializeXML()
        {
            var xml = new XmlSerializer(typeof(ObservableCollection<Car>));

            using (var file = new FileStream("Cars.xml", FileMode.OpenOrCreate))
            {
                Cars = (ObservableCollection<Car>?)xml.Deserialize(file);
            }
        }
    }
}
