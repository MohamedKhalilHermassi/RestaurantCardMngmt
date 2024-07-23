using System.ComponentModel.DataAnnotations;

namespace RM.CarteResto.Model.Entitiy
{
    public class CarteRestaurant
    {
        public Guid Id { get; set; }
        [Key]
        public string PartitionKey { get; set; }
        public string Numero { get; set; }
        public float Solde { get; set; }

        public string? UserId { get; set; }
        public string? UserEmail { get; set; }

        public string[] TransactionIds { get; set; }

        public CarteRestaurant()
        {
            Id = Guid.NewGuid();
            PartitionKey= Id.ToString();

        }
    }
}
