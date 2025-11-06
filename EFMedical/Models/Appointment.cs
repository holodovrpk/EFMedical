using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMedical.Models
{
    public class Appointment
    {
        // Первичный ключ таблицы (идентификатор приёма)
        [Key]
        public int AppointmentId { get; set; }

        // Дата и время приёма (обязательное поле)
        [Required]
        public DateTime Date { get; set; }

        // Заметки о приёме (необязательное поле, максимум 500 символов)
        [MaxLength(500)]
        public string? Notes { get; set; }

        // Внешний ключ — идентификатор врача, который проводит приём
        public int DoctorId { get; set; }

        // Навигационное свойство — ссылка на врача (Doctor)
        // Атрибут ForeignKey связывает это свойство с полем DoctorId
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        // Внешний ключ — идентификатор пациента, который пришёл на приём
        public int PatientId { get; set; }

        // Навигационное свойство — ссылка на пациента (Patient)
        // Атрибут ForeignKey связывает это свойство с полем PatientId
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
