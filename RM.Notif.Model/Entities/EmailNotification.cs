using System.ComponentModel.DataAnnotations;

namespace RM.Notif.Model.Entities
{
    public class EmailNotification
    {
  
        public  Guid Id { get; set; }
        [Key]
        public string PartitionKey { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public bool Statut { get; set; }
        public string StatutError { get; set; }

        public EmailNotification()
        {
            Id = Guid.NewGuid();
            PartitionKey = Id.ToString();
        }

    }

}
