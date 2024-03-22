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

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void ButtonToOrder(object sender, RoutedEventArgs e)
        {
            this.Close();
            Order order = new Order();
            order.Show();
            this.Hide();
        }

        private void PastaToOrder(object sender, RoutedEventArgs e)
        {
           
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = PastaName.Text;
            quantity.DishPriceTextBlock.Text = PastaPrice.Text;
        }
        private void FreeToOrder(object sender, RoutedEventArgs e)
        {
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = FreeName.Text;
            quantity.DishPriceTextBlock.Text = FreePrice.Text;

        }
        private void SteakToOrder(object sender, RoutedEventArgs e)
        {
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = SteakName.Text;
            quantity.DishPriceTextBlock.Text = SteakPrice.Text;

        }
        private void PizzaToOrder(object sender, RoutedEventArgs e)
        {
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = PizzaName.Text;
            quantity.DishPriceTextBlock.Text = PizzaPrice.Text;

        }
        private void SaladToOrder(object sender, RoutedEventArgs e)
        {
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = SaladName.Text;
            quantity.DishPriceTextBlock.Text = SaladPrice.Text;

        }
        private void SoupToOrder(object sender, RoutedEventArgs e)
        {
            Quantity quantity = new Quantity();
            quantity.Show();
            quantity.DishNameTextBlock.Text = SoupName.Text;
            quantity.DishPriceTextBlock.Text = SoupPrice.Text;

        }

    }
}
