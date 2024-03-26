using RestaurantWPF;
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
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Data;

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для Quantity.xaml
    /// </summary>
    public partial class Quantity : Window
    {

        private SqlConnection connection = new SqlConnection();
        public Quantity()
        {
            InitializeComponent();
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {            
            Manager manager = new Manager();
            manager.AddToOrder(DishNameTextBlock.Text, Convert.ToInt32(QuantityTextBox.Text));
            Hide();
            this.Close();
        }
        private void PlusQuantityClick(object sender, RoutedEventArgs e)
        {
            QuantityTextBox.Text = (Convert.ToInt16(this.QuantityTextBox.Text) + 1).ToString();
            MinusQuantityButton.IsEnabled = true;
        }

        private void MinusQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt16(QuantityTextBox.Text) > 0)
            {
                QuantityTextBox.Text = (Convert.ToInt16(this.QuantityTextBox.Text) - 1).ToString();
            }
            else
            {
                MinusQuantityButton.IsEnabled = false;
            }
        }
    }
}
