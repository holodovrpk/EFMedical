using EFMedical.Models;
using System.Windows.Controls;

namespace EFMedical.Pages
{
    // Класс страницы "Главная панель"
    // Отображает статистику: количество врачей, пациентов и приёмов
    public partial class HomePage : Page
    {
        // Конструктор страницы
        // В качестве параметра принимает контекст базы данных (ClinicContext)
        public HomePage(ClinicContext _db)
        {
            // Инициализация компонентов интерфейса (создаёт элементы, определённые в XAML)
            InitializeComponent();

            // Получаем количество записей в таблице врачей
            // _db.Doctors.ToList().Count() — загружает всех врачей и считает количество
            // .ToString() — переводит число в строку для отображения в TextBlock
            DoctorsCountText.Text = _db.Doctors.ToList().Count().ToString();

            // Получаем количество пациентов
            PatientsCountText.Text = _db.Patients.ToList().Count().ToString();

            // Получаем количество приёмов
            AppointmentsCountText.Text = _db.Appointments.ToList().Count().ToString();
        }
    }
}
