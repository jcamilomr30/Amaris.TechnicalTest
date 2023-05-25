using System.Runtime.Serialization;

namespace Amaris.TechnicalTest.Domain.Entities
{
    public class EntityBase<TId> where TId : struct
    {
        [DataMember(Name = "id")]
        public TId Id { get; set; }
    }
}
