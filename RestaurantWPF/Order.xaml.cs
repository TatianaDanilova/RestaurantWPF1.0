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
using static RestaurantWPF.DBEntities;
using System.Windows.Markup;

namespace RestaurantWPF
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        
        string connectionString = "Data Source=DESKTOP-4HJV1J2;Initial Catalog=RestaurantDB;Integrated Security=True";

        public Order()
        {
            InitializeComponent();
            LoadOrderDetails();

        }
        private void LoadOrderDetails()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM OrderDetailsView";
                string query1 = "SELECT * FROM TotalOrderCostView";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                SqlDataAdapter adapter1 = new SqlDataAdapter(query1, connection);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);


                orderDetailsGrid.ItemsSource = dataTable.DefaultView;
                TotalCost.ItemsSource = dataTable1.DefaultView;

            }
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
            string caption = "";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Вы уверены в том, что хотите завершить заказ?", caption, buttons, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Вы успешно завершили заказ! До встречи!");
                System.Windows.Application.Current.Shutdown();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "delete from OrderItems";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    orderDetailsGrid.ItemsSource = dataTable.DefaultView;
                }
            }
        }


        private void PlusQuantityClick(object sender, RoutedEventArgs e)
        {
            // Получаем текущую строку в DataGrid, на которой находится кнопка
            DataRowView rowView = (DataRowView)((Button)e.Source).DataContext;

            // Получаем текущее значение количества порций из TextBox
            int currentQuantity = Convert.ToInt32(rowView["Количество порций"]);

            // Уменьшаем количество порций на 1
            currentQuantity++;

            // Обновляем значение количества порций в TextBox
            rowView["Количество порций"] = currentQuantity;

            // Обновляем значение количества порций в таблице OrderItems
            UpdateQuantityInDatabase(currentQuantity, rowView);

            LoadOrderDetails();
        }

        private void MinusQuantityButtonClick(object sender, RoutedEventArgs e)
        {
            // Получаем текущую строку в DataGrid, на которой находится кнопка
            DataRowView rowView = (DataRowView)((Button)e.Source).DataContext;

            // Получаем текущее значение количества порций из TextBox
            int currentQuantity = Convert.ToInt32(rowView["Количество порций"]);

            if (currentQuantity > 1)
            {
                // Уменьшаем количество порций на 1
                currentQuantity--;

                // Обновляем значение количества порций в TextBox
                rowView["Количество порций"] = currentQuantity;

                // Обновляем значение количества порций в таблице OrderItems
                UpdateQuantityInDatabase(currentQuantity, rowView);

                LoadOrderDetails();
            }
            else
            {
                DeleteOrderItem(rowView);
                LoadOrderDetails();
                MessageBox.Show("Блюдо удалено из заказа");
            }
        }

        private void UpdateQuantityInDatabase(int newQuantity, DataRowView rowView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateOrderItemQuantity", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@dish_name", System.Data.SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@newQuantity", System.Data.SqlDbType.Int));
                    cmd.Parameters["@dish_name"].Value = rowView["Название блюда"];
                    cmd.Parameters["@newQuantity"].Value = newQuantity;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void DeleteOrderItem(DataRowView rowView)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteOrderItem", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@dish_name", System.Data.SqlDbType.NVarChar, 100));
                    cmd.Parameters["@dish_name"].Value = rowView["Название блюда"];

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


