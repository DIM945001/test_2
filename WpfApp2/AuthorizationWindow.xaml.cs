using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

        }
        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");
            // создаём таблицу в приложении

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=DESKTOP-7CFGRQJ\\SQLEXPRESS;" + "Initial catalog=Trade;" + "Integrated Security=SSPI;";
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом

            return dataTable;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length > 0) // проверяем введён ли логин     
            {
                if (PassBox.Password.Length > 0) // проверяем введён ли пароль         
                {             // ищем в базе данных пользователя с такими данными         
                    DataTable dt_user = Select("SELECT * FROM [dbo].[User] WHERE [Userlogin] = '" + LoginBox.Text + "' AND [UserPassword] = '" + PassBox.Password + "'");
                    if (dt_user.Rows.Count > 0) // если такая запись существует       
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                    }
                    else MessageBox.Show("Не удалось найти пользователя");
                }
            }
        }
    }
}
