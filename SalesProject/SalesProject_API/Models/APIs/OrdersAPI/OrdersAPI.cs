using SalesProject.Models.Modules.Customers;
using SalesProject.Models.Modules.Orders;
using SalesProject.Models.Modules.Products;
using SalesProject_API.Models.APIBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesProject_API.Models.APIs.OrdersAPI
{
    public class OrdersAPI
    {

        public OrdersResponseAPI GetMyOrders(OrdersRequestAPI ordersRequestAPI)
        {
            OrdersResponseAPI ordersResponseAPI = new OrdersResponseAPI();
            OrdersCOM oOrdersCOM = new OrdersCOM();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(ordersRequestAPI.APIToken);
            ordersResponseAPI.lstOrders = oOrdersCOM.GetMyOrders((int)CustomerID, ordersRequestAPI.LanguageID);
            if (ordersResponseAPI.lstOrders != null && ordersResponseAPI.lstOrders.Count() > 0)
            {
                ordersResponseAPI.TotalOrdersAmount = ordersResponseAPI.lstOrders.Sum(x => x.TotalAmount);
            }
            ordersResponseAPI.APIStatus = 1;
            ordersResponseAPI.APIMessage = "Success";
            return ordersResponseAPI;
        }

        public OrdersResponseAPI GetUserOrders(OrdersRequestAPI ordersRequestAPI)
        {
            OrdersResponseAPI ordersResponseAPI = new OrdersResponseAPI();
            OrdersCOM oOrdersCOM = new OrdersCOM();
            ordersResponseAPI.lstOrders = oOrdersCOM.GetMyOrders(ordersRequestAPI.CustomerID, ordersRequestAPI.LanguageID);
            if (ordersResponseAPI.lstOrders != null && ordersResponseAPI.lstOrders.Count() > 0)
            {
                ordersResponseAPI.TotalOrdersAmount = ordersResponseAPI.lstOrders.Sum(x => x.TotalAmount);
            }
            ordersResponseAPI.APIStatus = 1;
            ordersResponseAPI.APIMessage = "Success";
            return ordersResponseAPI;
        }


        public OrdersResponseAPI GetOrderDetails(OrdersRequestAPI ordersRequestAPI)
        {
            OrdersResponseAPI orderResponseAPI = new OrdersResponseAPI();
            OrdersCOM oOrdersCOM = new OrdersCOM();
            orderResponseAPI.oOrder = oOrdersCOM.GetOrderDetails(ordersRequestAPI.OrderID, ordersRequestAPI.LanguageID);
            orderResponseAPI.APIStatus = 1;
            orderResponseAPI.APIMessage = "Success";
            return orderResponseAPI;
        }

        public OrdersResponseAPI SaveOrder(OrdersRequestAPI ordersRequestAPI)
        {
            OrdersResponseAPI ordersResponseAPI = new OrdersResponseAPI();
            OrdersCOM oOrdersCOM = new OrdersCOM();
            //CustomersCOM oCustomersCOM = new CustomersCOM();
            //long CustomerID = oCustomersCOM.GetCustomerIDByToken(ordersRequestAPI.APIToken);

            ordersResponseAPI.oOrder = oOrdersCOM.SaveOrder(ordersRequestAPI.oOrdersModel, ordersRequestAPI.LanguageID);
            if (ordersResponseAPI.oOrder.lstUnAvailableProducts.Count() == 0)
            {
                ordersResponseAPI.APIStatus = 1;
                ordersResponseAPI.APIMessage = "Success";
            }
            else
            {
                ordersResponseAPI.lstUnAvailableItems = ordersResponseAPI.oOrder.lstUnAvailableProducts;
                ordersResponseAPI.APIStatus = 1;
                ordersResponseAPI.APIMessage = "Error";
            }
            return ordersResponseAPI;
        }

        public OrdersResponseAPI ChangeOrderStatus(OrdersRequestAPI ordersRequestAPI)
        {
            OrdersResponseAPI orderResponseAPI = new OrdersResponseAPI();
            OrdersCOM oOrdersCOM = new OrdersCOM();
            orderResponseAPI.oOrder = oOrdersCOM.ChangeOrderStatus(ordersRequestAPI.OrderID, ordersRequestAPI.OrderStatusID, ordersRequestAPI.LanguageID);
            orderResponseAPI.APIStatus = 1;
            orderResponseAPI.APIMessage = "Success";
            return orderResponseAPI;
        }
    }
    public class OrdersRequestAPI : APIModel
    {
        public int OrderStatus { get; set; }
        public long OrderID { get; set; }
        public long CustomerID { get; set; }
        public int OrderStatusID { get; set; }


        public OrdersModel oOrdersModel { get; set; }

    }

    public class OrdersResponseAPI : APIModel
    {
        public decimal TotalOrdersAmount { get; set; }
        public List<OrdersModel> lstOrders { get; set; }

        public OrdersModel oOrder { get; set; }
        public List<ProductsModel> lstUnAvailableItems { get; set; }
    }

}