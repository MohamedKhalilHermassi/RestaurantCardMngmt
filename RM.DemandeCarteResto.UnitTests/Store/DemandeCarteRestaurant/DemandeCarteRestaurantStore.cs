using RM.DemandeCarteResto.Model;

namespace RM.DemandeCarteResto.UnitTests
{
    internal class DemandeCarteRestaurantStore
    {
        internal DemandeCarteRestaurant DemandeCarteValid()
        {
            return new DemandeCarteRestaurant()
            {
                Date = DateTime.Today,
                PartitionKey = "456",
                Status = false,
            };
        }
    }
}
