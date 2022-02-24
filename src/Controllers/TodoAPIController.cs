using System;
using ASPNETMongoDBExample.Models;
using ASPNETMongoDBExample.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETMongoDBExample.Controllers
{
	[ApiController]
	[Route("api/todo")]
	public class TodoAPIController : Controller
	{
		private readonly ITodoRepository _todoRepository;

		public TodoAPIController(ITodoRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var todo = await _todoRepository.Get(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepository.GetAll();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Todo todo)
        {
            await _todoRepository.Insert(todo);

            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Todo todo)
        {
            var existingTodo = await _todoRepository.Get(id);

            if (existingTodo == null)
                return NotFound();

            todo.Id = existingTodo.Id;

            var result = await _todoRepository.Update(todo);

            // You probably want a better exception/logging than this
            // in a real application.
            if (!result)
                throw new Exception("Error updating Todo item");

            return Ok(todo);
        }

        [HttpPut("order")]
        public async Task<IActionResult> PutOrder(List<Todo> todos)
        {
            if (todos == null || todos.Count < 1)
                return Ok();

            int currentOrder = 0;

            // In a production application, you might want to perform
            // the logic below in a batch rather than individually.
            foreach (var todo in todos)
            {
                if (!string.IsNullOrEmpty(todo.Id))
                {
                    var existingTodo = await _todoRepository.Get(todo.Id);

                    if (existingTodo == null)
                        return NotFound();

                    existingTodo.Order = currentOrder;
                    var result = await _todoRepository.UpdateOrder(existingTodo);

                    // You probably want a better exception/logging than this
                    // in a real application.
                    if (!result)
                        throw new Exception("Error updating Todo item order!");

                    currentOrder += 1;
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTodo = await _todoRepository.Get(id);

            if (existingTodo == null)
                return NotFound();

            var result = await _todoRepository.Delete(existingTodo);

            // You probably want a better exception/logging than this
            // in a real application.
            if (!result)
                throw new Exception("Error trying to delete Todo item");

            return Ok();
        }
    }
}

