using System;
using System.Collections.Generic;

namespace Assignment1
{
    /// <summary>
    /// Label describing the TodoItem.
    /// e.g. Critical, Personal, Work...
    /// </summary>
    /// 
    public class TodoItemLabel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// All TodoItems that are associated with this label
        /// </summary>
        public List<TodoItem> LabelTodoItems { get; set; }

        public TodoItemLabel()
        {

        }

        public TodoItemLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }

        protected bool Equals(TodoItemLabel other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoItemLabel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}