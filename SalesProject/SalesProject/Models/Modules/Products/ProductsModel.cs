using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.Modules.Products
{
    public class ProductsModel
    {
        public long ProductID { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        
        public bool SoldOut { get; set; }
        public DateTime PostedDate { get; set; }
        

    }
}