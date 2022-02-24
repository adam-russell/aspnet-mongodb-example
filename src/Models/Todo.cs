using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASPNETMongoDBExample.Models
{
	// You'll likely want to use BsonIgnoreExtraElements on real
	// classes you're using with MongoDB so that if you change models
	// (e.g., you remove a property), retrieving documents won't throw
	// an exception if there's an extra element in the actual document
	// in MongoDB
	[BsonIgnoreExtraElements]
	public class Todo
	{
		// The BsonRepresentation(BsonType.ObjectId) attribute is the
		// most important here. It lets us use the Id as a string in
		// .NET code, including in serialization without extra code,
		// and it treats it as an ObjectId (including autogenerating
		// new values on inserts) when dealing with MongoDB.
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		public string? Name { get; set; }

		public bool Done { get; set; }

		public int? Order { get; set; }

		[JsonIgnore]
		public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

		[JsonIgnore]
		public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;
	}
}

