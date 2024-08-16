using Xunit;

namespace RM.CarteResto.UnitTests
{
    public class GetCardByUserIdQueryTests
    {
        [Fact]
        public async Task GetCardByUserId_ShouldReturnValidCarteRestaurant_WhenCardExists()
        {
            var _getCardByUserIdQuery = new GetCardByUserIdQueryBuilder()
                .WithUserId()
                .WithPartitionKey()
                .Build();

            var result = await _getCardByUserIdQuery.ExecuteAsync(Values.USER_ID);

            Assert.NotNull(result);
            Assert.Equal(Values.USER_ID, result.UserId);
        }


        [Fact]
        public async Task GetCardByUserId_ShouldThrowArgumentException_WhenUserIdIsNull()
        {
            var getCardByUserId = new GetCardByUserIdQueryBuilder()
                                      .Build();                            

            await Assert.ThrowsAsync<ArgumentException>(() => getCardByUserId.ExecuteAsync(null));
        }
    }
}
