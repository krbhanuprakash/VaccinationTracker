using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VaccinationTracker.Models
{
    public class VaccinationSchedule
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // Updated to match IdentityUser.Id type
        public Guid VaccineId { get; set; }
        public DateTime ScheduledDate { get; set; }
        [Required]
        [EnumDataType(typeof(ScheduleStatus))]
        public ScheduleStatus Status { get; set; } // Updated to use an enum for status
        public string CertificateUrl { get; set; }
        public IdentityUser? User { get; set; } // Navigation property for User
        public Vaccine? Vaccine { get; set; } // Navigation property for Vaccine
    }

    public enum ScheduleStatus
    {
        Pending,
        Active,
        Completed
    }
}

