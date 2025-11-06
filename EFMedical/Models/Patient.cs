using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFMedical.Models
{
    public class Patient
    {
        // Первичный ключ таблицы (идентификатор пациента)
        [Key]
        public int PatientId { get; set; }

        // Полное имя пациента (обязательно для заполнения, максимум 100 символов)
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        // Дата рождения пациента
        public DateTime DateOfBirth { get; set; }

        // Телефон (необязательное поле, максимум 50 символов)
        [MaxLength(50)]
        public string Phone { get; set; }

        // Адрес проживания (необязательное поле, максимум 200 символов)
        [MaxLength(200)]
        public string Address { get; set; }

        // Навигационное свойство — список всех приёмов, связанных с этим пациентом
        // (один пациент может иметь несколько приёмов)
        public ICollection<Appointment> Appointments { get; set; }
    }
}
