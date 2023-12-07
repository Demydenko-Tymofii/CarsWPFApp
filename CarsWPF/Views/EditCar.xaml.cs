using CarsWPF.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarsWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для EditCar.xaml
    /// </summary>
    public partial class EditCar : Window
    {
        public EditCar(MainWindow? win)
        {
            InitializeComponent();
            DataContext = new EditCarViewModel((win?.DataContext as MainWindowViewModel)?.SelectedCar);
        }
    }
}
