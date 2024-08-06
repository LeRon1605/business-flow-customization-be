using System.Numerics;
using BuildingBlocks.Infrastructure.Cdc.Serializers;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.Infrastructure.Cdc.Models;

public class CaptureTableStateModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    [BsonSerializer(typeof(BigIntSerializer))]
    public BigInteger LastProcessedLsn { get; set; }
}