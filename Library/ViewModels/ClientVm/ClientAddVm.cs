using Library.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ClientVm
{
    public class ClientAddVm : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        public string Family { get; set; }

    }
}