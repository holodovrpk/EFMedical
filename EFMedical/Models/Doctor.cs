using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFMedical.Models
{
    public class Doctor
    {
        // Первичный ключ таблицы (идентификатор врача)
        [Key]
        public int DoctorId { get; set; }

        // Полное имя врача (обязательно для заполнения, максимум 100 символов)
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        // Специальность врача (например: терапевт, хирург и т.д.), максимум 100 символов
        [MaxLength(100)]
        public string Specialty { get; set; }

        // Номер телефона врача (необязательное поле, максимум 50 символов)
        [MaxLength(50)]
        public string Phone { get; set; }

        // Навигационное свойство — список приёмов, проведённых этим врачом
        // (один врач может иметь множество приёмов)
        public ICollection<Appointment> Appointments { get; set; }
    }
}
