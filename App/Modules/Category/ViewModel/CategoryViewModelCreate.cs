using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_bookStore.App.Modules.Category.ViewModel
{
    public class CategoryViewModelCreate(string name, string description)
    {
        [Required]
        public string Name { get; set; } = name;
        [Required]
        public string Description { get; set; } = description;

    }
}