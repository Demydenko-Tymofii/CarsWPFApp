using CarsWPF.Infrastructure;
using CarsWPF.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsWPF.ModelViews
{
    class EditCarViewModel: INotifyPropertyChanged
    {
        Car? selectedCar;
        public Car? SelectedCar
        {
            get => selectedCar;
            set
            {
                if(selectedCar != value)
                {
                    selectedCar = value;
                }
            }
        }

        public string? CarImage
        {
            get => selectedCar?.Image;
            set
            {
                if (selectedCar?.Image != value)
                {
                    File.Copy(value, Directory.GetCurrentDirectory() + "/Image/" + Path.GetFileName(value), true);
                    selectedCar.Image = Directory.GetCurrentDirectory() + "/Image/" + Path.GetFileName(value);
                    OnPropertyChanged();
                }
            }
        }
        public EditCarViewModel(Car? selectCar)
        {
            SelectedCar = selectCar;
            CarImage = selectedCar?.Image;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
