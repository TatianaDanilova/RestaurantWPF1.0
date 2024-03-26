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
        string connectionString = "Data Source=DESKTOP-4HJV1J2;Initial Catalog=RestaurantDB;Integrated Security=True";

        public int CheckName()
        {
            int idUser = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CheckName", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@first_name", System.Data.SqlDbType.NVarChar, 100);
                command.Parameters.Add("@last_name", System.Data.SqlDbType.NVarChar, 100);
                command.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 100);
                command.Parameters.Add("@client_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters["@first_name"].Value = FirstNameBox.Text;
                command.Parameters["@last_name"].Value = LastNameBox.Text;
                command.Parameters["@pass"].Value = PasswordBox.Password;

                connection.Open();
                command.ExecuteNonQuery();

                object idUserObj = command.Parameters["@client_id"].Value;
                if (idUserObj != DBNull.Value)
                {
                    idUser = Convert.ToInt32(idUserObj);
                }
            }
            return idUser;
        }
        public void Check()
        {
            string first_name = FirstNameBox.Text.Trim();
            string last_name = LastNameBox.Text.Trim();
            string pattern = "^[a-zA-Zа-яА-Я]+$";

            if (!Regex.IsMatch(first_name, pattern))
            {
                FirstNameBox.Background = Brushes.IndianRed;
                FirstNameBox.ToolTip = "Введите имя, используя только буквы";
            }
            else
            {
                FirstNameBox.Background = Brushes.Transparent;
            }

            if (!Regex.IsMatch(last_name, pattern))
            {
                LastNameBox.Background = Brushes.IndianRed;
                LastNameBox.ToolTip = "Введите фамилию, используя только буквы";
            }
            else
            {
                LastNameBox.Background = Brushes.Transparent;
            }
            if (PasswordBox.Password.Contains(" "))
            {
                PasswordBox.Background = Brushes.IndianRed;
                PasswordBox.ToolTip = "В пароле не должны использоваться пробелы";
            }
            else
            {
                PasswordBox.Background = Brushes.Transparent;
            }
        }

        

        private void RegestrationClick(object sender, RoutedEventArgs e)
        { 

        Check();
            // Все данные введены корректно

            if ((FirstNameBox.Background == Brushes.Transparent) && (LastNameBox.Background == Brushes.Transparent) && (PasswordBox.Background == Brushes.Transparent))
            {
                // Пользователь есть в бд
                int userId = CheckName();
                if (userId != 0)
                {
                    MessageBox.Show("Вы уже зарегестрированы");
                }
                // Пользователя нет в бд
                else
                {
                    // Добавление пользователя в бд
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("AddClient", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@first_name", System.Data.SqlDbType.NVarChar, 100));
                            cmd.Parameters.Add(new SqlParameter("@last_name", System.Data.SqlDbType.NVarChar, 100));
                            cmd.Parameters.Add(new SqlParameter("@pass", System.Data.SqlDbType.NVarChar, 100));
                            cmd.Parameters["@first_name"].Value = FirstNameBox.Text;
                            cmd.Parameters["@last_name"].Value = LastNameBox.Text;
                            cmd.Parameters["@pass"].Value = PasswordBox.Password;

                            cmd.ExecuteNonQuery();
                        }
                        try
                        {
                            MessageBox.Show("Вы успешно зарегестрировались");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}");

                        }
                    }
                }
            }
        }  

        private void EntryClick(object sender, RoutedEventArgs e)
        {
            Check();
            int userId = CheckName();
            if(userId != 0)
            {
                AppSettings.UserId = userId;
                AppSettings.UserFirstName = FirstNameBox.Text;
                AppSettings.UserLastName = LastNameBox.Text;

                Menu menu = new Menu();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверное имя, фамилия или пароль");
            }
        }


        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if (!string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    // Введены оба имени и пароль, активируем кнопки
                    RegestratoinButton.IsEnabled = true;
                    EntryButton.IsEnabled = true;
                }
                else
                {
                    // Если что-то не введено, деактивируем кнопки
                    RegestratoinButton.IsEnabled = false;
                    EntryButton.IsEnabled = false;
                }
            }
        }
        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if (!string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    // Введены оба имени и пароль, активируем кнопки
                    RegestratoinButton.IsEnabled = true;
                    EntryButton.IsEnabled = true;
                }
                else
                {
                    // Если что-то не введено, деактивируем кнопки
                    RegestratoinButton.IsEnabled = false;
                    EntryButton.IsEnabled = false;
                }
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if (!string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    // Введены оба имени и пароль, активируем кнопки
                    RegestratoinButton.IsEnabled = true;
                    EntryButton.IsEnabled = true;
                }
                else
                {
                    // Если что-то не введено, деактивируем кнопки
                    RegestratoinButton.IsEnabled = false;
                    EntryButton.IsEnabled = false;
                }
            }
        }

    }
}
