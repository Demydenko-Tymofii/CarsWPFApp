using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsWPF.Models
{
    public class Car: INotifyPropertyChanged
    {
        string? brand;
        string? color;
        int maxSpeed;
        string? image;

        public string? Brand
        {
            get => brand;
            set
            {
                if (brand != value)
                {
                    brand = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxSpeed 
        {
            get => maxSpeed;
            set
            {
                if(maxSpeed != value)
                {
                    maxSpeed = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Image
        {
            get => image;
            set
            {
                if (image != value)
                {
                    image = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
