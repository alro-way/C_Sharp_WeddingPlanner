using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_WeddingPlanner.Models 
{
    public class User 
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be 2 characters or longer!")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be 2 characters or longer!")]
        public string LastName {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password {get;set;}
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
// ONE To MANY for Creator
        // public List<Wedding> CreatedWeddings {get;set;}
// MANY TO MANY for Guest
        public List<Association> WeddingJoined {get;set;}
    }
}