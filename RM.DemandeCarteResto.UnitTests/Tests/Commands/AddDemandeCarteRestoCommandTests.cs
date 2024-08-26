using Xunit;

namespace RM.DemandeCarteResto.UnitTests.Tests.Commands
{
    public class AddDemandeCarteRestoCommandTests
    {
        [Fact]
        public async Task AddDemandeCardCommand_Test()
        {
            var addCardCommand = new AddDemandeCarteRestoBuilder()
                        .WithPartitionKey()
                        .WithDate()
                        .WithStatus()
                        .Build();

            var store = new DemandeCarteRestaurantStore();

            // déclaration de la carte à ajouter 
            var demande = store.DemandeCarteValid();
            await addCardCommand.ExecuteAsync(demande);

            Assert.Equal(Values.PARTITION_KEY, demande.PartitionKey);
            Assert.Equal(Values.Status, demande.Status);
            Assert.Equal(Values.DATE, demande.Date);

        }
    }
}
