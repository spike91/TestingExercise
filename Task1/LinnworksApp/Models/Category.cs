using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_TestApp2.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductsCount { get; set; }
    }
}