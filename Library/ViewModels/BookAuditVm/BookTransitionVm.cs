using Library.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Library.ViewModels.BookAuditVm 
{
    public class BookTransitionVm : IEntity
    {
        [Display(Name = "Запись")]
        public int Id { get; set; }

        [Display(Name = "Книга")]
        [Required]
        public int? BookId { get; set; }

        [Display(Name = "Клиент")]
        [Required]
        public int? ClientId { get; set; }

        public IEnumerable<SelectListItem> BookSelectList { get; set; }

        public IEnumerable<SelectListItem> ClientSelectList { get; set; }

        public BookTransitionVm()
        {
            BookSelectList = new List<SelectListItem>();

            ClientSelectList = new List<SelectListItem>();
        }
    }
}