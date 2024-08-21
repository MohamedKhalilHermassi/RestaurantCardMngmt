using RM.CarteResto.Business;
using RM.CarteResto.UnitTests.Builders.Command;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace RM.CarteResto.UnitTests.Tests.Command
{
    public class RemoveCardCommandTests
    {
        [Fact]
        public async Task RemoveCardCommand_Test()
        {
            var removeCardBuilder = new RemoveCardCommandBuilder()
                                    .WithPartitionKey()
                                    .Build();
            var _getCardByUserIdQuery = new GetCardByUserIdQueryBuilder()
                                     .WithUserId()
                                     .WithPartitionKey()
                                     .Build();

            var store = new CarteRestaurantStore();
            var card = store.CardToRemove();
            var removedCard = _getCardByUserIdQuery.ExecuteAsync(card.PartitionKey);

            Assert.Null(removedCard);
            

        }

    }
}
