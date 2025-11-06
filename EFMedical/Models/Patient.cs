using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMedical.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        // Навигационное свойство — список приёмов
        public ICollection<Appointment> Appointments { get; set; }
    }
}
