using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_Products
    {
        [Key]
        public long ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool SoldOut { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime PostedDate { get; set; }
        public int NumberOfViews { get; set; }
        
    }
}