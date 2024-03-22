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

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для Quantity.xaml
    /// </summary>
    public partial class Quantity : Window
    {

        private OrderItems _currentOrderItem = new OrderItems();

        public Quantity()
        {
            InitializeComponent();


            DataContext = _currentOrderItem;
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            bool hasRecords = RestaurantEntities.GetContext().OrderItems.Any();


            if (hasRecords)
            {
                int maxId = RestaurantEntities.GetContext().OrderItems.Max(item => item.id);
                
                // Установите значения свойств модели OrderItems
                _currentOrderItem.id = maxId + 1;
                _currentOrderItem.dishName = DishNameTextBlock.Text;
                _currentOrderItem.quantity = Convert.ToInt32(QuantityTextBox.Text);
                _currentOrderItem.price = Convert.ToInt32(DishPriceTextBlock.Text) * _currentOrderItem.quantity;
                RestaurantEntities.GetContext().OrderItems.Add(_currentOrderItem);   
            }
            else
            {
                _currentOrderItem.id = 1;
                _currentOrderItem.dishName = DishNameTextBlock.Text;
                _currentOrderItem.quantity = Convert.ToInt32(QuantityTextBox.Text);
                _currentOrderItem.price = Convert.ToInt32(DishPriceTextBlock.Text) * Convert.ToInt32(QuantityTextBox.Text);
                RestaurantEntities.GetContext().OrderItems.Add(_currentOrderItem);
            }


            try
            {
                RestaurantEntities.GetContext().SaveChanges();
                MessageBox.Show("Блюдо успешно добавлено в заказ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                
            }

            

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
