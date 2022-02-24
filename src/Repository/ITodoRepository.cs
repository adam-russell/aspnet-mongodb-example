using System;
using ASPNETMongoDBExample.Models;

namespace ASPNETMongoDBExample.Repository
{
	public interface ITodoRepository
	{
		Task<Todo> Get(string id);
		Task<IEnumerable<Todo>> GetAll();
		Task Insert(Todo value);
		Task<bool> Update(Todo value);
		Task<bool> UpdateOrder(Todo value);
		Task<bool> Delete(Todo value);
	}
}

