using Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.BookVm
{
    public class BookEditDescriptionVm : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}