using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyDemoProject001.Domain.Common
{
    public abstract class AuditableEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
