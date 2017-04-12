using System;
using System.ComponentModel.DataAnnotations;

namespace myWall.Models
{

    public class Questions
    {
        public int ID { get; set; }

        [Display(Name = "Question: "), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = ""), Required]
        public string Question { get; set; }

        [Display(Name = "Answer: "), Required]
        public string Answer { get; set; }

        [Display(Name = "Answer Hint:"), Required]
        public string Hint { get; set; }

        [Display(Name = "Date:"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}"), Required]
        public DateTime Date { get; set; }

        [Display(Name = "Email:"), Required]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}; output = {Question} {Answer} {Hint} {Date} {Email}";
        }
    }
}