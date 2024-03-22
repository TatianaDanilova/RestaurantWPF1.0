using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        


        public MainWindow()
        {
            InitializeComponent();
        }

        public static Menu menu;

        private void ButtonToMenu(object sender, RoutedEventArgs e)
        {
            string first_name = FirstNameBox.Text.Trim();
            string last_name = LastNameBox.Text.Trim();
            string pattern = "^[a-zA-Zа-яА-Я]+$";

            if (!Regex.IsMatch(first_name, pattern))
            {
                FirstNameBox.Background = Brushes.DarkRed;
                FirstNameBox.ToolTip = "Введите имя, используя только буквы";
            } 
            else
            {
                FirstNameBox.Background = Brushes.Transparent;
            }
            
            if (!Regex.IsMatch(last_name, pattern))
            {
                LastNameBox.Background = Brushes.DarkRed;
                LastNameBox.ToolTip = "Введите фамилию, используя только буквы";
            }
            else
            {
                LastNameBox.Background = Brushes.Transparent;
            }

            if ((menu == null) && (FirstNameBox.Background == Brushes.Transparent) && (LastNameBox.Background == Brushes.Transparent))
            {
                menu = new Menu();
                menu.Show();
                this.Hide();
            }
        }  

        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FirstNameBox.Text) && !string.IsNullOrWhiteSpace(LastNameBox.Text))
            {
                // Введены оба имени, активируем кнопку
                RegestratoinButton.IsEnabled = true;
            }
            else
            {
                // Если что-то не введено, деактивируем кнопку
                RegestratoinButton.IsEnabled = false;
            }
        }
        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FirstNameBox.Text) && !string.IsNullOrWhiteSpace(LastNameBox.Text))
            {
                // Введены оба имени, активируем кнопку
                RegestratoinButton.IsEnabled = true;
            }
            else
            {
                // Если что-то не введено, деактивируем кнопку
                RegestratoinButton.IsEnabled = false;
            }
        }
    }
}
