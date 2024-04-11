using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.BookVm
{
    public class BookAddVm : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}
