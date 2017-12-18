using System;
using Assignment1;

namespace Assignment2.Models.TodoViewModels
{
    public class TodoItemModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime? DateDue { get; set; }
        public string TimeLeft { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItemModel(TodoItem todoItem)
        {
            Id = todoItem.Id;
            Text = todoItem.Text;
            DateDue = todoItem.DateDue;
            TimeLeft = getTimeLeftMessage(todoItem.DateDue);
            IsCompleted = todoItem.IsCompleted;
        }

        private static String getTimeLeftMessage(DateTime? DateDue)
        {
            string timeLeft;
            if (!DateDue.HasValue)
            {
                timeLeft = "Ne može se odrediti!";
            }
            else
            {
                DateTime dateTime = DateDue.GetValueOrDefault();
                DateTime currentDateTime = DateTime.Now;

                TimeSpan diffSpan = dateTime - currentDateTime;

                if (diffSpan.Ticks < 0)
                {
                    timeLeft = "Vrijeme je isteklo!";
                }
                else
                {
                    timeLeft = "Za ";
                    if (diffSpan.TotalDays > 0)
                    {
                        timeLeft += diffSpan.TotalDays + " dana i ";
                    }
                    timeLeft += diffSpan.Hours + " sati!";
                }

            }
            return timeLeft;
        }
    }
}