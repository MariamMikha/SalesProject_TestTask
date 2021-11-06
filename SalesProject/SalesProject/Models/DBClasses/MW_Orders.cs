using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_Orders
    {
        [Key]
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public decimal SubAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderNotes { get; set; }
        public int OrderStatusID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }


        [ForeignKey("OrderStatusID")]
        public MW_OrderStatus MW_OrderStatus { get; set; }

        public virtual ICollection<MW_OrdersDetails> MW_OrdersDetails { get; set; }
    }
}