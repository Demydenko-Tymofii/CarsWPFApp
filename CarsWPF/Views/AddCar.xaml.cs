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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public AddCar(MainWindow? win)
        {
            InitializeComponent();
            DataContext = new AddCarViewModel((win?.DataContext as MainWindowViewModel)?.Cars);
        }
    }
}
