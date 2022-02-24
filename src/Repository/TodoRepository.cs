using System;
using System.Linq;
using ASPNETMongoDBExample.Context;
using ASPNETMongoDBExample.Models;
using MongoDB.Driver;

namespace ASPNETMongoDBExample.Repository
{
public class TodoRepository : ITodoRepository
{
	private readonly IExampleMongoContext _context;

	public TodoRepository(IExampleMongoContext context)
	{
		_context = context;
	}

	public Task<Todo> Get(string id)
    {
		var filter = Builders<Todo>.Filter.Eq(t => t.Id, id);

		return _context.TodoItems.Find(filter: filter).FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<Todo>> GetAll()
	{
		var values = await _context
				.TodoItems
				.Find(_ => true)
				.ToListAsync();

		// Note, we want to sort first by the nullable Order field
		// with nulls after values, thus we're doing the sort
		// in memory vs. in MongoDB in the statement above.
		return values
			.OrderBy(t => t.Order == null)
			.ThenBy(t => t.Order)
			.ThenBy(t => t.CreatedUtc);
	}

	public Task Insert(Todo value)
	{
		return _context.TodoItems.InsertOneAsync(value);
	}

	public async Task<bool> Update(Todo value)
    {
		if (string.IsNullOrEmpty(value.Id))
			throw new ArgumentOutOfRangeException(nameof(value));

		var filter = Builders<Todo>.Filter.Eq(t => t.Id, value.Id);

		var updateDefinition =
			Builders<Todo>.Update
				.Set(t => t.Name, value.Name)
				.Set(t => t.Done, value.Done)
				.Set(t => t.LastModifiedUtc, DateTime.UtcNow);

		var updateResult =
			await _context
					.TodoItems
					.UpdateOneAsync(
						filter: filter,
						update: updateDefinition);

		return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
	}

	public async Task<bool> UpdateOrder(Todo value)
	{
		if (string.IsNullOrEmpty(value.Id))
			throw new ArgumentOutOfRangeException(nameof(value));

		var filter = Builders<Todo>.Filter.Eq(t => t.Id, value.Id);

		var updateDefinition =
			Builders<Todo>.Update
				.Set(t => t.Order, value.Order)
				.Set(t => t.LastModifiedUtc, DateTime.UtcNow);

		var updateResult =
			await _context
					.TodoItems
					.UpdateOneAsync(
						filter: filter,
						update: updateDefinition);

		return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
	}

	public async Task<bool> Delete(Todo value)
    {
		if (string.IsNullOrEmpty(value.Id))
			throw new ArgumentOutOfRangeException(nameof(value));

		var filter = Builders<Todo>.Filter.Eq(t => t.Id, value.Id);

		var deleteResult =
			await _context
					.TodoItems
					.DeleteOneAsync(filter: filter);

		return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
	}
}
}

