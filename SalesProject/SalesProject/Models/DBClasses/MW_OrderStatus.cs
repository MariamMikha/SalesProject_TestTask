using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_OrderStatus
    {
        [Key]
        public int OrderStatusID { get; set; }

        public string OrderStatusName { get; set; }
    }
}