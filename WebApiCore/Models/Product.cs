using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCore.Models
{
    /// <summary>
    /// A Product Class
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}