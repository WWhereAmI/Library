using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.BookVm
{
    public class BookAddVm : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Автор")]
        [Required]
        public string Author { get; set; }
    }
}
