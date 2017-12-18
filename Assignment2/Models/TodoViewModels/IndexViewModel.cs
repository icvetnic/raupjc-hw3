using System.Collections.Generic;
using Assignment1;

namespace Assignment2.Models.TodoViewModels
{
    public class IndexViewModel
    {
        public List<TodoItemModel> TodoItemModels { get; set; }

        public IndexViewModel(List<TodoItem> todoItems)
        {
            TodoItemModels = new List<TodoItemModel>();

            foreach (TodoItem todoItem in todoItems)
            {
                TodoItemModels.Add(new TodoItemModel(todoItem));
            }
        }

    }
}