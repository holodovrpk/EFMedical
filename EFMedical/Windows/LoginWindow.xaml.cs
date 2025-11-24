using EFMedical.Models;
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

namespace EFMedical.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ClinicContext database = new ClinicContext();

            string login = LoginBox.Text;
            string pass = PassBox.Text;

            var user = database.Users.
                FirstOrDefault(u => u.Login == login && u.Password == pass);
            if (user == null)
            {
                MessageBox.Show("Ошибка в логин/пароль");
                return;
            }

            MainWindow w = new MainWindow();
            w.Title += $" ({user.Login} - {user.Role})";

            if (user.Role == "Врач")
            {
                //w.ButtonDoctor.IsEnabled = false;
                w.ButtonDoctor.Visibility = Visibility.Collapsed;
            }

            w.Show();

            this.Close();


        }
    }
}
