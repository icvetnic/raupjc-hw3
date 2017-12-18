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

        [StringLength(100, ErrorMessage = "Labeles must be separated with ;")]
        [RegularExpression(@"^((\w[\w -]*)|((\w[\w -]*;)+(\w[\w -]*)))$")]
        public String Labels { get; set; }

        public String[] separateLabels()
        {
            if (Labels == null)
            {
                return null;
            }
            String[] labels = Labels.Split(new char[] { ';' });
            return labels;
        }
    }
}
