using Library.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ClientVm
{
    public class ClientAddVm : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Family { get; set; }

    }
}