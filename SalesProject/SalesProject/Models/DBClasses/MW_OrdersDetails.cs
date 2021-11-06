using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_OrdersDetails
    {
        [Key]
        public long ID { get; set; }
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("OrderID")]
        public MW_Orders MW_Orders { get; set; }
    }
}