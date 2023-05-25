using System.Runtime.Serialization;

namespace Amaris.TechnicalTest.Core.Dtos
{
    public class EntityBase<TId> where TId : struct
    {
        [DataMember(Name = "id")]
        public virtual TId Id { get; set; }
    }
}
