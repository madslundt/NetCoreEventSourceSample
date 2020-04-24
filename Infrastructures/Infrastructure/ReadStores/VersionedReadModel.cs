using EventFlow.ReadStores;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ReadStores
{
    public class VersionedReadModel : IReadModel
    {
        [Key]
        public string AggregateId { get; protected set; }

        [ConcurrencyCheck]
        public long Version { get; set; }
    }
}
