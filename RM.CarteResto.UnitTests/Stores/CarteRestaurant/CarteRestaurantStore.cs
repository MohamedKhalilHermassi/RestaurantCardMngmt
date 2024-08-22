using RM.CarteResto.Model;

namespace RM.CarteResto.UnitTests
{
    internal class CarteRestaurantStore
    {

         internal CarteRestaurant validCarteRestaurant()
            {
                return new CarteRestaurant();
            }

        internal CarteRestaurant validCardToAdd()
        {
            return new CarteRestaurant()
            {
                PartitionKey= "1234",
                Solde=150,
                Numero= "12345"
            };
        }

        internal CarteRestaurant CardToRemove()
        {
            return new CarteRestaurant()
            {
                PartitionKey = "987",
                Solde = 150,
                Numero = "98765"
            };
        }
    }
}
