using EFMedical.Models;
using EFMedical.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EFMedical.Pages
{
    // Класс страницы "Врачи"
    // Управляет отображением и редактированием списка врачей
    public partial class DoctorsPage : Page
    {
        // Поле для хранения контекста базы данных (EF Core)
        // Через него выполняются все запросы и сохранения
        ClinicContext _db;

        // Коллекция врачей, которая автоматически обновляет интерфейс при изменении (ObservableCollection)
        public ObservableCollection<Doctor> doctors { get; set; }

        // Конструктор страницы — вызывается при открытии DoctorsPage
        public DoctorsPage(ClinicContext db)
        {
            InitializeComponent(); // создаёт элементы интерфейса из XAML

            // Сохраняем переданный из MainWindow контекст базы данных
            _db = db;

            // Загружаем данные таблицы "Doctors" из базы в локальный кэш EF
            // После вызова Load() EF хранит эти данные в _db.Doctors.Local
            _db.Doctors.Load();

            // Преобразуем локальную коллекцию EF в ObservableCollection
            // Это нужно, чтобы WPF автоматически обновлял DataGrid при добавлении/удалении врачей
            doctors = _db.Doctors.Local.ToObservableCollection();

            // Устанавливаем источник данных для DataGrid — коллекцию врачей
            DoctorsGrid.ItemsSource = doctors;
        }

        // Событие выбора строки в таблице
        private void DoctorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, выбран ли объект типа Doctor
            if (DoctorsGrid.SelectedItem is Doctor d)
            {
                // Назначаем выбранного врача в DataContext
                // Это позволяет полям TextBox (FullName, Specialty, Phone) автоматически отображать данные выбранного врача
                DataContext = d;
            }
        }

        // Сохранение изменений
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем все изменения, сделанные пользователем, в базе данных
            // EF автоматически отслеживает изменения в сущностях (если они связаны с контекстом)
            _db.SaveChanges();

            // Можно дополнительно вывести сообщение пользователю
            // MessageBox.Show("Изменения сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Добавление нового врача
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Создаём новый объект врача (пустой)
            Doctor NewDoctor = new Doctor();

            // Открываем отдельное окно AddDoctorWindow
            // В DataContext этого окна передаём новый объект врача (для привязки полей)
            AddDoctorWindow w = new AddDoctorWindow() { DataContext = NewDoctor };

            // Если пользователь нажал "Сохранить" в AddDoctorWindow (DialogResult == true)
            if (w.ShowDialog() == true)
            {
                // Добавляем нового врача в коллекцию doctors
                // Поскольку doctors — ObservableCollection, таблица обновится сразу
                doctors.Add(NewDoctor);
            }

            // Сохраняем изменения в базе
            _db.SaveChanges();
        }

        // Удаление выбранного врача
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли врач
            if (DoctorsGrid.SelectedItem is Doctor d)
            {
                // Удаляем врача из коллекции doctors
                // Так как коллекция привязана к DataGrid, строка автоматически исчезнет из таблицы
                doctors.Remove(d);
            }

            // Сохраняем изменения в базе данных
            _db.SaveChanges();
        }
    }
}
