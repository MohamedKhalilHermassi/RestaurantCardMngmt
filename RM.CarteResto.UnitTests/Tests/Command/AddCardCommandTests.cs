using Moq;
using RM.CarteResto.Model;
using Xunit;

namespace RM.CarteResto.UnitTests.Tests.Command
{
    public class AddCardCommandTests
    {
        [Fact]
        public async Task AddCardCommand_Test()
        {
            var addCardCommand = new AddCardCommandBuilder()
                        .WithPartitionKey()
                        .WithNumero()
                        .WithSolde()
                        .Build();
          
            var store = new CarteRestaurantStore();

            // déclaration de la carte à ajouter 
            var card = store.validCardToAdd();
            await addCardCommand.ExecuteAsync(card);

            Assert.Equal(Values.PARTITIONKEY, card.PartitionKey);
            Assert.Equal(Values.SOLDE, card.Solde);
            Assert.Equal(Values.NUMERO, card.Numero);

        }

    }
}
