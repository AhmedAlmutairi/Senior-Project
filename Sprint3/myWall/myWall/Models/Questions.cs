using System;
using System.ComponentModel.DataAnnotations;

namespace myWall.Models
{

    public class Questions
    {
        public int ID { get; set; }

        [Display(Name = "Question: "), Required]
        public string Question { get; set; }

        [Display(Name = "Answer: "), Required]
        public string Answer { get; set; }

        [Display(Name = "Answer Hint:"), Required]
        public string Hint { get; set; }
    }
}