using System.ComponentModel.DataAnnotations;


namespace RM.Notifications.Model
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        [Key]
        public string PartitionKey { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; set; }
        public bool Read { get; set; }

        public Notification()
        {
            NotificationId = new Guid();
            PartitionKey = NotificationId.ToString();
            Date = DateTime.Now;
            Read = false;   
        }
    }
}
