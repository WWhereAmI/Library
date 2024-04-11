using Library.Const;
using System;

namespace Library.Models
{
    public class BookAudit
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int ClientId { get; set; }

        public string Book { get; set; }    

        public string Client {  get; set; } 
    }
}