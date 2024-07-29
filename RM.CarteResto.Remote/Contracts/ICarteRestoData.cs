using System.Runtime.Serialization;

namespace Remote
{
        [DataContract]
        public class CarteRestoByIdRequest
        {
            [DataMember(Order = 1)]
            public string PartitionKey { get; set; }
        }

        [DataContract]
        public class CarteRestoByIdReply
        {
            [DataMember(Order = 1)]
            public Guid Id { get; set; }

            [DataMember(Order = 2)]
            public string Numero { get; set; }

            [DataMember(Order = 3)]
            public float Solde { get; set; }

            [DataMember(Order = 4)]
            public string[] TransactionIds { get; set; }

            [DataMember(Order = 5)]
            public string UserId { get; set; }

            [DataMember(Order = 6)]
            public string UserEmail { get; set; }
            [DataMember(Order = 7)]
            public string PartitionKey { get; set; }
            public CarteRestoByIdReply()
                 {
                     Id = Guid.NewGuid();
                     PartitionKey = Id.ToString();
                  }
    }

        [DataContract]
        public class AllCartesRestoReply
        {
            [DataMember(Order = 1)]
            public IEnumerable<CarteRestoByIdReply> CartesRestaurant { get; set; }
        }
    }
