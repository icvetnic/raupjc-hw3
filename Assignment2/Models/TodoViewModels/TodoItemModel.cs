using System;
using Assignment1;

namespace Assignment2.Models.TodoViewModels
{
    public class TodoItemModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public TodoItemModel(TodoItem todoItem)
        {
            
        }
    }
}