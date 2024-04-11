using Library.Const;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public BookStatus Status { get; set; }
    }
}