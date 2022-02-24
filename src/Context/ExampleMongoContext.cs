using System;
using ASPNETMongoDBExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ASPNETMongoDBExample.Context
{
	public class ExampleMongoContext : IExampleMongoContext
	{
		private readonly IMongoDatabase _db;

		public IMongoCollection<Todo> TodoItems => _db.GetCollection<Todo>("TodoItems");

		public ExampleMongoContext(IOptions<MongoDbSettings> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);
			_db = client.GetDatabase(options.Value.DatabaseName);
		}
	}
}

