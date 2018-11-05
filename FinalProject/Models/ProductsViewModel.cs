using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProductsViewModel
    {
        public ProductTypeViewModel[] ProductTypes { get; set; }

        

    }

    public class ProductTypeViewModel
    {
        public string Name { get; set; }

        public string[] AvailablesGenres { get; set; }
        public Product[] Products { get; set; }
        public ProductProductGenre[] ProductProductGenres { get; set; }
    }
}
