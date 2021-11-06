using SalesProject.Models.DBClasses;
using SalesProject.Models.Modules.Customers;
using SalesProject.Models.Modules.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.Modules.Orders
{
    public class OrdersModel
    {
        public OrdersModel()
        {
            ocustomer = new CustomersModel();
            oOrderStatus = new MW_OrderStatus();
            lstOrderDetails = new List<OrdersDetailsModel>();
            lstUnAvailableProducts = new List<ProductsModel>();
        }
        public long OrderID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public long CustomerID { get; set; }
        public CustomersModel ocustomer { get; set; }
        public decimal SubAmount { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public decimal DiscountAmount { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public decimal ServiceCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderNotes { get; set; }
        public int OrderStatusID { get; set; }
        public string OrderStatusName { get; set; }
        public MW_OrderStatus oOrderStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrdersDetailsModel> lstOrderDetails { get; set; }

        public List<ProductsModel> lstUnAvailableProducts { get; set; }
    }

    public class OrdersDetailsModel
    {
        public long OrderDetailsID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string MainImage { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
       
    }
}