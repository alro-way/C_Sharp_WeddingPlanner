using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_WeddingPlanner.Models 
{
    public class Wedding 
    {
        [Key]
        public int WeddingId {get;set;}
        [Required]
        public string NameOne {get;set;}
        [Required]
        public string NameTwo {get;set;}
        [Required]
        [OnlyFutureDate]
        public DateTime Date {get;set;}
        [Required]
        public string Address {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
// One TO MANY
        public int UserId {get;set;}
        public User Creator {get;set;}
// MANY TO MANY
        public List<Association> WeddingGuests {get;set;}
    }

     public class OnlyFutureDateAttribute: ValidationAttribute 
     {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if ((DateTime) value < DateTime.Now)
                return new ValidationResult("Date must be in the Future");
            return ValidationResult.Success;
        }

    }


}