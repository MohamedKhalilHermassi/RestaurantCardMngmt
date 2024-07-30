using System.ComponentModel.DataAnnotations;

namespace RM.DemandeCarteResto.Model
{
    public class DemandeCarteRestaurant
    {
        public Guid Id { get; set; }
        [Key]
        public string PartitionKey { get; set; }
        public DateTime Date { get; set; }
        public bool? Status { get; set; }

        public string UserId { get; set; }
        public string? UserEmail { get; set; }

        public DemandeCarteRestaurant()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Status = null;
            PartitionKey = Id.ToString();
        }
    }
}
