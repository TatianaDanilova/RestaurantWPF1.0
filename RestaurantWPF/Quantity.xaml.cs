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
        private OrderItems _currentOrderItem = new OrderItems();
        private MenuItems _currentMenuItem = new MenuItems();

        private SqlConnection connection = new SqlConnection();
        public Quantity()
        {
            InitializeComponent();
            DataContext = _currentMenuItem;
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            Order order = new Order();

            // Установите значения свойств модели OrderItems
            _currentMenuItem.dish_name = DishNameTextBlock.Text;
            _currentOrderItem.quantity = Convert.ToInt32(QuantityTextBox.Text);
            _currentMenuItem.dish_price = Convert.ToInt32(DishPriceTextBlock.Text) * _currentOrderItem.quantity;
            
            string connectionString = "Data Source=DESKTOP-4HJV1J2;Initial Catalog=RestaurantDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("AddOrderItem", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@dish_name", System.Data.SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@quantity", System.Data.SqlDbType.Int));
                    cmd.Parameters["@dish_name"].Value = DishNameTextBlock.Text;
                    cmd.Parameters["@quantity"].Value = Convert.ToInt32(QuantityTextBox.Text);

                    cmd.ExecuteNonQuery();
                }
                try
                {
              //  DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Блюдо успешно добавлено в заказ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");

                }

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
