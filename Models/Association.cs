using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_WeddingPlanner.Models 
{
    public class Association
    {
        [Key]
        public int AsId {get;set;}
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        public User ToBeGuest {get;set;}
        public Wedding ToJoinWedding {get;set;}

    }
}