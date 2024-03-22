using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Reflection.Emit;
using static RestaurantWPF.RestaurantEntities;
using System.Windows.Markup;

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private readonly OrderItems _currentOrderItem = new OrderItems();
        public Order()
        {
            InitializeComponent();
            OrderName.ItemsSource = RestaurantEntities.GetContext().OrderItems.ToList();
            DataContext = _currentOrderItem;
           
        }

        private void ButtonToMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void Break(object sender, RoutedEventArgs e)
        {
            var orderItems = RestaurantEntities.GetContext().OrderItems;

            if (orderItems.Any())
            {
                // Если в базе есть записи, удаляем их все
                RestaurantEntities.GetContext().OrderItems.RemoveRange(orderItems);
                RestaurantEntities.GetContext().SaveChanges();

                System.Windows.Application.Current.Shutdown();

            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        
        private void PlusQuantityClick(object sender, RoutedEventArgs e)
        {

            var button = (Button)sender;
            var dataContext = button.DataContext as OrderItems;
            int dishprice = dataContext.price / dataContext.quantity;
          

            if (dataContext != null)
            {
                dataContext.quantity += 1;
                dataContext.price += dishprice;

                OrderName.Items.Refresh();
            }

        }


        private void MinusQuantityButtonClick(object sender, RoutedEventArgs e)
        {
            var dishToRemove = OrderName.SelectedItems.Cast<OrderItems>().ToList();
            var button = (Button)sender;
            var dataContext = button.DataContext as OrderItems;
            int dishprice = dataContext.price / dataContext.quantity;


            if (dataContext != null)
            {
                if (dataContext.quantity > 0)
                {
                    dataContext.quantity -= 1;
                    dataContext.price -= dishprice;

                    if (dataContext.quantity == 0)
                    {
                        
                        RestaurantEntities.GetContext().OrderItems.RemoveRange(dishToRemove);
                        RestaurantEntities.GetContext().SaveChanges();
                        OrderName.ItemsSource = RestaurantEntities.GetContext().OrderItems.ToList();
                        MessageBox.Show("Блюдо удалено из заказа");

                    }

                    RestaurantEntities.GetContext().SaveChanges();
                    OrderName.Items.Refresh();
                }
            }
        }
    }
}


