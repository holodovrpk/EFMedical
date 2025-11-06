using Microsoft.EntityFrameworkCore;

namespace EFMedical.Models
{
    // Класс контекста базы данных — основной мост между приложением и SQL-базой
    public class ClinicContext : DbContext
    {
        // Таблица врачей
        public DbSet<Doctor> Doctors { get; set; }

        // Таблица пациентов
        public DbSet<Patient> Patients { get; set; }

        // Таблица приёмов
        public DbSet<Appointment> Appointments { get; set; }

        // Настройка подключения к базе данных
        // Здесь используется LocalDB (локальная база SQL Server)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Строка подключения указывает:
            // - Server=(localdb)\mssqllocaldb — встроенный SQL Server для разработки
            // - Database=ClinicDB — имя базы данных
            // - Trusted_Connection=True — использовать учётную запись Windows
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ClinicDB;Trusted_Connection=True;");
        }

        // Конструктор контекста
        public ClinicContext()
        {
            // Проверка существования базы данных
            // Если базы нет — EF создаст её автоматически на основе моделей
            Database.EnsureCreated();
        }
    }
}
