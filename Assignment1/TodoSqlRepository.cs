using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }


        public async Task<TodoItem> Get(Guid todoId, Guid userId)
        {
            TodoItem todoItem = await _context.TodoItems
                    .Include(td => td.Labels)
                    .FirstOrDefaultAsync(td => td.Id.Equals(todoId));

            if (todoItem == null)
            {
                return null;
            }
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(
                    $"User with id:{userId} is not the owner of the todoItem with id:{todoId}.");
            }
            return todoItem;
        }

        public async void Add(TodoItem todoItem)
        {
            if (_context.TodoItems.Select(td => td.Id).Contains(todoItem.Id))
            {
                throw new DuplicateTodoItemException(
                    $"TodoItem with id:{todoItem.Id} is already in the data base.");
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public async Task<bool> Remove(Guid todoId, Guid userId)
        {
            TodoItem todoItem;
            if ((todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(td => td.Id.Equals(todoId))) == null)
            {
                return false;
            }
            if (!todoItem.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(
                    $"User with id:{userId} is not the owner of the todoItem with id:{todoId}.");
            }
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return true;
        }

        public async void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem dBtodoItem = await Get(todoItem.Id, userId);
            if (dBtodoItem == null)
            {
                Add(todoItem);
            }
            else
            {
                if (!dBtodoItem.UserId.Equals(userId))
                {
                    throw new TodoAccessDeniedException(
                        $"User with id:{userId} is not the owner of the todoItem with id:{dBtodoItem.Id}.");
                }
                _context.Entry(todoItem).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public async Task<bool> MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem todoItem = await Get(todoId, userId);

            if (todoItem == null)
            {
                return false;
            }
            if (todoItem.IsCompleted == true)
            {
                return true;
            }
            todoItem.MarkAsCompleted();
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TodoItem>> GetAll(Guid userId)
        {
            return await _context.TodoItems
                .Where(td => td.UserId.Equals(userId))
                .OrderByDescending(td => td.DateCreated)
                .ToListAsync();
        }

        public async Task<List<TodoItem>> GetActive(Guid userId)
        {
            return await _context.TodoItems
                .Where(td => td.IsCompleted == false && td.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<List<TodoItem>> GetCompleted(Guid userId)
        {
            return await _context.TodoItems
                .Where(td => td.IsCompleted == true && td.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<List<TodoItem>> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return await _context.TodoItems
                .Where(td => filterFunction(td) && td.UserId.Equals(userId))
                .ToListAsync();
        }
    }
}