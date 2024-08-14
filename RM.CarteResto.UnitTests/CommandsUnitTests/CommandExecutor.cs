using RM.CarteResto.Business;

namespace RM.CarteResto.UnitTests.CommandsUnitTests
{
    public class CommandExecutor
    {
        public static async Task ExecuteAsync(RemoveCardCommand command, string partitionKey)
        {
            await command.ExecuteAsync(partitionKey);
        }
    }
}
