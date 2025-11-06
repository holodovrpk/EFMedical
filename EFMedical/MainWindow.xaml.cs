using EFMedical.Models;
using System.Windows;

namespace EFMedical
{
    public partial class MainWindow : Window
    {
        // Создаём экземпляр контекста базы данных (EF)
        // Через него будут выполняться все операции с таблицами (врачи, пациенты, приёмы)
        ClinicContext database = new ClinicContext();

        // Конструктор главного окна
        public MainWindow()
        {
            InitializeComponent();

            // После инициализации интерфейса сразу загружаем страницу "Главная"
            // и передаём в неё контекст базы данных
            MainFrame.Navigate(new Pages.HomePage(database));
        }

        // ===== Обработчики кнопок меню =====

        // Кнопка "Главная" — открывает страницу с общей статистикой (HomePage)
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.HomePage(database));
        }

        // Кнопка "Врачи" — открывает страницу со списком врачей (DoctorsPage)
        private void BtnDoctors_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.DoctorsPage(database));
        }

        // Кнопка "Пациенты" — открывает страницу пациентов
        // (пока закомментировано, чтобы не вызывало ошибок, если страницы нет)
        private void BtnPatients_Click(object sender, RoutedEventArgs e)
        {
            // MainFrame.Navigate(new Pages.PatientsPage(database));
        }

        // Кнопка "Приёмы" — открывает страницу управления приёмами (AppointmentsPage)
        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.AppointmentsPage(database));
        }

        // Кнопка "Выход" — завершает работу программы
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            // Спрашиваем подтверждение у пользователя
            if (MessageBox.Show("Выйти из программы?", "Выход",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Если пользователь подтвердил — закрываем приложение
                Application.Current.Shutdown();
            }
        }
    }
}
