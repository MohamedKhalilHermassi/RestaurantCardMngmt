using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Google.Protobuf.WellKnownTypes;
using Remote;
namespace GrpcTransactionClient;
internal class Program
{
    private static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7093");
        var client = channel.CreateGrpcService<ICarteRestoService>();
        var added = await client.addCarteResto(
            new CarteRestoByIdReply { Numero = "1098", Solde = 150, TransactionIds = [ Guid.NewGuid().ToString()] });

        Console.WriteLine($"Added CarteResto");
        Console.WriteLine($"Id : {added.Id}");
        Console.WriteLine($"Numero : {added.Numero}");
        Console.WriteLine($"Solde : {added.Solde}");
        foreach (var transactionId in added.TransactionIds)
        {
            Console.WriteLine($"id : {transactionId}");

        }
        Console.WriteLine("Get All CarteResto method : ");

        var allcards = client.GetAllCarteResto(new Empty());
        foreach (CarteRestoByIdReply carte in allcards.Result.CartesRestaurant)
        {
            Console.WriteLine($"id : {carte.Id}");
            Console.WriteLine($"Solde : {carte.Solde}");
            Console.WriteLine($"Numero : {carte.Numero}");
            Console.WriteLine("Transactions Ids");
            foreach (var transactionId in carte.TransactionIds)
            {
                Console.WriteLine($"id : {transactionId}");
            }
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}