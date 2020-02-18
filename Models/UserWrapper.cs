using System.ComponentModel.DataAnnotations; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace C_Sharp_WeddingPlanner.Models 
{ 
    public class UserWrapper
    { 
        public User NewUser {get;set;}
        public Login NewLogin {get;set;} 
    } 
}