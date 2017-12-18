using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.TodoViewModels
{
    public class AddTodoViewModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "You must put some text here!")]
        public string Text { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateDue { get; set; }
    }
}
