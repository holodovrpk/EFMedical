using EFMedical.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EFMedical.Pages
{
    // Класс страницы "Приёмы"
    // Управляет отображением, редактированием и сохранением данных о приёмах
    public partial class AppointmentsPage : Page
    {
        // Контекст базы данных (через него работаем с EF Core)
        ClinicContext _db;

        // ObservableCollection — коллекция приёмов, автоматически обновляет UI при изменении
        public ObservableCollection<Appointment> appointments { get; set; }

        // Отдельные списки для врачей и пациентов
        // Используются для заполнения ComboBox в форме редактирования
        public List<Patient> patients { get; set; }
        public List<Doctor> doctors { get; set; }

        // Конструктор страницы
        // Принимает объект контекста базы данных, чтобы работать с одной БД в рамках всего приложения
        public AppointmentsPage(ClinicContext db)
        {
            InitializeComponent(); // создаёт визуальные элементы из XAML

            // Сохраняем переданный контекст базы данных
            _db = db;

            // Загружаем таблицу приёмов вместе с навигационными свойствами Doctor и Patient
            // Include(...) — загружает связанные данные (JOIN), чтобы в DataGrid сразу показывались имена врачей и пациентов
            _db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Load();

            // Конвертируем локальные данные EF в ObservableCollection
            // Это позволит автоматически обновлять DataGrid при добавлении или удалении приёмов
            appointments = _db.Appointments.Local.ToObservableCollection();

            // Загружаем врачей и пациентов из базы в обычные списки
            // Они нужны для ComboBox (списков выбора врача и пациента)
            patients = _db.Patients.ToList();
            doctors = _db.Doctors.ToList();

            // Привязываем коллекции к элементам интерфейса
            AppointmentsGrid.ItemsSource = appointments; // таблица приёмов
            DoctorBox.ItemsSource = doctors;             // выпадающий список врачей
            PatientBox.ItemsSource = patients;           // выпадающий список пациентов
        }

        // ============================ ДОБАВЛЕНИЕ ПРИЁМА ============================
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно реализовать создание нового приёма в отдельнос окне
            
            // Пока метод пустой — логика добавления реализуется позже
        }

        // ============================ СОХРАНЕНИЕ ИЗМЕНЕНИЙ ============================
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем все изменения, сделанные пользователем, в базу данных
            _db.SaveChanges();

            // Обновляем DataGrid вручную
            // Иногда EF не отслеживает моментальное изменение навигационных свойств (Doctor, Patient)
            // поэтому Items.Refresh() обновляет визуальную часть интерфейса
            AppointmentsGrid.Items.Refresh();
        }

        // ============================ УДАЛЕНИЕ ПРИЁМА ============================
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно реализовать удаление выбранного приёма
            // Например:
            //
            // if (AppointmentsGrid.SelectedItem is Appointment selected)
            // {
            //     if (MessageBox.Show("Удалить выбранный приём?", "Подтверждение",
            //                         MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //     {
            //         appointments.Remove(selected);
            //         _db.SaveChanges();
            //     }
            // }
            //
            // Пока метод пустой, чтобы не вызывать ошибки
        }

        // ============================ ВЫБОР СТРОКИ В ТАБЛИЦЕ ============================
        private void AppointmentsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, выбран ли объект типа Appointment
            if (AppointmentsGrid.SelectedItem is Appointment a)
            {
                // Устанавливаем выбранный приём как DataContext страницы
                // Это позволяет полям внизу (DatePicker, ComboBox, TextBox)
                // автоматически подставлять значения выбранного приёма через Binding
                DataContext = a;
            }
        }
    }
}
