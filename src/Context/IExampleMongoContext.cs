using System;
using ASPNETMongoDBExample.Models;
using MongoDB.Driver;

namespace ASPNETMongoDBExample.Context
{
	public interface IExampleMongoContext
	{
		IMongoCollection<Todo> TodoItems { get; }
	}
}

