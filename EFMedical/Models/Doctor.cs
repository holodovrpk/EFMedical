using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMedical.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        // Навигационное свойство — список приёмов
        public ICollection<Appointment> Appointments { get; set; }
    }
}
