using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1;

namespace Assignment2.Models.TodoViewModels
{
    public class CompletedViewModel
    {
        public List<TodoItemModel> TodoItemModels { get; set; }

        public CompletedViewModel(List<TodoItem> todoItems)
        {
            TodoItemModels = new List<TodoItemModel>();

            foreach (TodoItem todoItem in todoItems)
            {
                TodoItemModels.Add(new TodoItemModel(todoItem));
            }
        }
    }
}
