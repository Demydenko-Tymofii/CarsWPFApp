using CarsWPF.Infrastructure;
using CarsWPF.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsWPF.ModelViews
{
    class AddCarViewModel: INotifyPropertyChanged
    {
        Car? newCar;
        public Car? NewCar 
        {
            get => newCar;
            set
            {
                if (newCar != value) 
                {
                    newCar = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? CarImage
        {
            get => newCar?.Image;
            set
            {
                if(newCar?.Image != value)
                {
                    newCar.Image = value;
                    OnPropertyChanged();
                }
            }
        }
        ObservableCollection<Car>? Cars { get; set; }
        public AddCarViewModel(ObservableCollection<Car>? cars)
        {
            Cars = cars;
            NewCar = new Car();
        }

        public AddCarViewModel()
        {
            NewCar = new Car();
        }

        ICommand? getImage;

        public ICommand GetImageCommand
        {
            get
            {
                return getImage ?? (getImage = new RelayCommand(GetImageMethod));
            }
        }

        void GetImageMethod(object param)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*|Image files (*.jpg;*.png)|*.jpg;*.png";
            fileDialog.FilterIndex = 2;
            if (fileDialog.ShowDialog() ?? false)
            {
                CarImage = fileDialog.FileName;
            }
        }

        ICommand? addCommand;

        public ICommand AddCarCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(AddCarMethod, CanAddCarMethod));
            }
        }

        void AddCarMethod(object param)
        {
            File.Copy(NewCar?.Image, Directory.GetCurrentDirectory() + "/Image/" + Path.GetFileName(NewCar?.Image), true);
            NewCar.Image = Directory.GetCurrentDirectory() + "/Image/" + Path.GetFileName(NewCar?.Image);
            Cars?.Add(NewCar);
            NewCar = new Car();
            CarImage = "";
        }

        bool CanAddCarMethod(object param)
        {
            return !(string.IsNullOrWhiteSpace(NewCar?.Brand) || string.IsNullOrWhiteSpace(NewCar?.Color) || NewCar?.MaxSpeed <= 0) && File.Exists(CarImage);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
