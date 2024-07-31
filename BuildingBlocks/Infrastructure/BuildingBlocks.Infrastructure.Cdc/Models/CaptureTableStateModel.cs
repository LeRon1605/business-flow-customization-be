using System.Numerics;
using BuildingBlocks.Infrastructure.Cdc.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlocks.Infrastructure.Cdc.Models;

public class CaptureTableStateModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    [BsonSerializer(typeof(BigIntSerializer))]
    public BigInteger LastProcessedLsn { get; set; }
}