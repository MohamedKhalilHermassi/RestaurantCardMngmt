using System.ComponentModel.DataAnnotations;

namespace RM.Transaction.Model.Entity
{
    public class Transactions
    {
        public Guid Id { get; set; }
        [Key]
        public string PartitionKey { get; set; }
        public DateTime Date { get; set; }
        public float Montant { get; set; }
        public string? Description { get; set; }
        public string CarteRestoId { get; set; }
        public bool? Type { get; set; }


        public Transactions()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            PartitionKey = Id.ToString();
        }
    }
}
