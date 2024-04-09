using Library.Const;
using System;

namespace Library.Models
{
    public class BookAudit
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public BookStatus Status { get; set; }

        public int ClientId { get; set; }

        public DateTime Took { get; set; }

        public DateTime Returned { get; set; }
    }
}