using System;
using System.Collections.Generic;

namespace Assignment1
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool IsCompleted => DateCompleted.HasValue;

        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// User id that owns this TodoItem
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// List of labels associated with TodoItem
        /// </summary>  
        public List<TodoItemLabel> Labels { get; set; }

        /// <summary>
        /// Date due. If null, no date was set by the user
        /// </summary>
        public DateTime? DateDue { get; set; }

        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        public TodoItem(string text)
        {
            // Generates new unique identifier
            Id = Guid.NewGuid();

            // DateTime .Now returns local time , it wont always be what you expect
            //(depending where the server is).
            // We want to use universal (UTC ) time which we can easily convert to
            //local when needed.
            // ( usually done in browser on the client side )
            DateCreated = DateTime.UtcNow;

            Text = text;
        }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public void ReplaceAllData(TodoItem todoItem)
        {
            Text = todoItem.Text;
            DateCompleted = todoItem.DateCompleted;
            DateCreated = todoItem.DateCreated;
        }

        protected bool Equals(TodoItem other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoItem) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}