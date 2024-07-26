using System.Runtime.Serialization;

namespace RM.Transaction.Remote.Contracts
{
    [DataContract]
    public class TransactionByIdRequest
    {
        [DataMember(Order = 1)]
        public string partitionkey { get; set; }
    }

    [DataContract]
    public class TransactionByIdReply
    {
        [DataMember(Order = 1)]
        public Guid Id { get; set; }

        [DataMember(Order = 2)]
        public DateTime Date { get; set; }

        [DataMember(Order = 3)]
        public float Montant { get; set; }

        [DataMember(Order = 4)]
        public string? Description { get; set; }

        [DataMember(Order = 5)]
        public string CarteRestoId { get; set; }
        [DataMember(Order = 6)]
        public bool Type { get; set; }
        [DataMember(Order = 7)]
        public string PartitionKey { get; set; }

    }


    
    [DataContract]
    public class AllTransactionsReply
    {
        [DataMember(Order = 1)]
        public IEnumerable<TransactionByIdReply> Transactions { get; set; }
    }
}
