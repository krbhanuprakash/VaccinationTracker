using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinationTracker.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        [NotMapped]
        public string? User { get; set; }
    }
}
