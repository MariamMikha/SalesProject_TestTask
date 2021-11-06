using SalesProject.Models.DBClasses;
using SalesProject.Models.Modules.Customers;
using SalesProject.Models.Modules.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SalesProject.Models.Modules.Orders
{
    public class OrdersCOM
    {
        public List<OrdersModel> GetOrders(int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                CustomersCOM oCustomersCOM = new CustomersCOM();
                List<OrdersModel> lstOrders = (from order in db.MW_Orders
                                               join orderdetails in db.MW_OrdersDetails on order.ID equals orderdetails.OrderID
                                               join orderstatus in db.MW_OrderStatus on order.OrderStatusID equals orderstatus.OrderStatusID
                                               join product in db.MW_Products on orderdetails.ProductID equals product.ID
                                               select new
                                               {
                                                   OrderID = order.ID,
                                                   CustomerID = order.CustomerID,
                                                   SubAmount = order.SubAmount,
                                                   DiscountAmount = order.DiscountAmount,
                                                   ServiceCharge = order.ServiceCharge,
                                                   TotalAmount = order.TotalAmount,
                                                   OrderNotes = order.OrderNotes,
                                                   OrderStatusID = orderstatus.OrderStatusID,
                                                   OrderStatusName = orderstatus.OrderStatusName,
                                                   OrderDetailsID = orderdetails.ID,
                                                   ProductID = orderdetails.ProductID,
                                                   Qty = orderdetails.Qty,
                                                   Price = orderdetails.Price,
                                                   TotalPrice = orderdetails.TotalPrice,
                                                   ProductName = product.ProductName,
                                                   ProductDescription = product.ProductDescription,
                                                   MainImage = product.MainImage,
                                                   CreatedDate = order.CreatedDate,
                                               }).AsEnumerable().GroupBy(x => x.OrderID).Select(order => new OrdersModel()
                                               {
                                                   CustomerID = order.FirstOrDefault().CustomerID,
                                                   DiscountAmount = order.FirstOrDefault().DiscountAmount,
                                                   OrderID = order.FirstOrDefault().OrderID,
                                                   OrderNotes = order.FirstOrDefault().OrderNotes,
                                                   OrderStatusID = order.FirstOrDefault().OrderStatusID,
                                                   OrderStatusName = order.FirstOrDefault().OrderStatusName,
                                                   ServiceCharge = order.FirstOrDefault().ServiceCharge,
                                                   SubAmount = order.FirstOrDefault().SubAmount,
                                                   TotalAmount = order.FirstOrDefault().TotalAmount,
                                                   ocustomer = oCustomersCOM.GetCustomerDetails(order.FirstOrDefault().CustomerID, LanguageID),
                                                   CreatedDate = order.FirstOrDefault().CreatedDate,
                                                   lstOrderDetails = (from item in order
                                                                      select new OrdersDetailsModel()
                                                                      {
                                                                          OrderDetailsID = item.OrderDetailsID,
                                                                          Price = item.Price,
                                                                          ProductID = item.ProductID,
                                                                          Qty = item.Qty,
                                                                          TotalPrice = item.TotalPrice,
                                                                          MainImage = item.MainImage,
                                                                          ProductDescription = item.ProductDescription,
                                                                          ProductName = item.ProductName
                                                                      }).ToList()
                                               }).OrderByDescending(x => x.CreatedDate).ToList();
                return lstOrders;
            }
        }

        public OrdersModel GetOrderDetails(long OrderID, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                CustomersCOM oCustomersCOM = new CustomersCOM();
                OrdersModel oOrder = (from order in db.MW_Orders
                                      join orderdetails in db.MW_OrdersDetails on order.ID equals orderdetails.OrderID
                                      join orderstatus in db.MW_OrderStatus on order.OrderStatusID equals orderstatus.OrderStatusID
                                      join product in db.MW_Products on orderdetails.ProductID equals product.ID
                                      where order.ID == OrderID
                                      select new
                                      {
                                          OrderID = order.ID,
                                          CustomerID = order.CustomerID,
                                          SubAmount = order.SubAmount,
                                          DiscountAmount = order.DiscountAmount,
                                          ServiceCharge = order.ServiceCharge,
                                          TotalAmount = order.TotalAmount,
                                          OrderNotes = order.OrderNotes,
                                          OrderStatusID = orderstatus.OrderStatusID,
                                          OrderStatusName = orderstatus.OrderStatusName,
                                          OrderDetailsID = orderdetails.ID,
                                          ProductID = orderdetails.ProductID,
                                          Qty = orderdetails.Qty,
                                          Price = orderdetails.Price,
                                          TotalPrice = orderdetails.TotalPrice,
                                          ProductName = product.ProductName,
                                          ProductDescription = product.ProductDescription,
                                          MainImage = product.MainImage,
                                          CreatedDate = order.CreatedDate,
                                      }).AsEnumerable().GroupBy(x => x.OrderID).Select(order => new OrdersModel()
                                      {
                                          CustomerID = order.FirstOrDefault().CustomerID,
                                          DiscountAmount = order.FirstOrDefault().DiscountAmount,
                                          OrderID = order.FirstOrDefault().OrderID,
                                          OrderNotes = order.FirstOrDefault().OrderNotes,
                                          OrderStatusID = order.FirstOrDefault().OrderStatusID,
                                          OrderStatusName = order.FirstOrDefault().OrderStatusName,
                                          ServiceCharge = order.FirstOrDefault().ServiceCharge,
                                          SubAmount = order.FirstOrDefault().SubAmount,
                                          TotalAmount = order.FirstOrDefault().TotalAmount,
                                          ocustomer = oCustomersCOM.GetCustomerDetails(order.FirstOrDefault().CustomerID, LanguageID),
                                          CreatedDate = order.FirstOrDefault().CreatedDate,
                                          lstOrderDetails = (from item in order
                                                             select new OrdersDetailsModel()
                                                             {
                                                                 OrderDetailsID = item.OrderDetailsID,
                                                                 Price = item.Price,
                                                                 ProductID = item.ProductID,
                                                                 Qty = item.Qty,
                                                                 TotalPrice = item.TotalPrice,
                                                                 MainImage = item.MainImage,
                                                                 ProductDescription = item.ProductDescription,
                                                                 ProductName = item.ProductName
                                                             }).ToList()
                                      }).FirstOrDefault();

                return oOrder;
            }
        }

        public OrdersModel SaveOrder(OrdersModel oModel, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                List<ProductsModel> unAvailableProducts = new List<ProductsModel>();

                MW_Orders oOrder = new MW_Orders();
                oOrder.CustomerID = oModel.CustomerID;
                oOrder.OrderNotes = oModel.OrderNotes;
                oOrder.OrderStatusID = oModel.OrderStatusID == 0 ? 1 : oOrder.OrderStatusID;
                oOrder.CreatedDate = DateTime.Now;


                oOrder.DiscountAmount = oModel.DiscountAmount;
                oOrder.ServiceCharge = oModel.ServiceCharge;
                //oOrder.SubAmount = oModel.SubAmount;
                //oOrder.TotalAmount = oModel.TotalAmount;
                //db.MW_Orders.Add(oOrder);
                //db.SaveChanges();

                List<MW_OrdersDetails> lstOrdersDetails = new List<MW_OrdersDetails>();

                foreach (var item in oModel.lstOrderDetails)
                {
                    MW_OrdersDetails oOrderDetails = new MW_OrdersDetails();
                    MW_Products oProduct = db.MW_Products.FirstOrDefault(x => x.ID == item.ProductID);
                    if (oProduct.Stock >= item.Qty && !oProduct.SoldOut)
                    {
                        oOrderDetails.OrderID = oOrder.ID;
                        oOrderDetails.Price = oProduct.Price;
                        oOrderDetails.ProductID = item.ProductID;
                        oOrderDetails.Qty = item.Qty;
                        oOrderDetails.TotalPrice = oOrderDetails.Qty * oOrderDetails.Price;
                        //db.MW_OrdersDetails.Add(oOrderDetails);
                        lstOrdersDetails.Add(oOrderDetails);
                        if (oProduct != null)
                        {
                            oProduct.Stock -= item.Qty;
                            if (oProduct.Stock == 0)
                            {
                                oProduct.SoldOut = true;
                            }
                        }

                        oOrder.SubAmount += oOrderDetails.TotalPrice;
                        db.SaveChanges();
                    }
                    else
                    {
                        ProductsModel product = new ProductsModel();
                        product.ProductID = oProduct.ID;
                        product.ProductName = oProduct.ProductName;
                        product.Stock = oProduct.Stock;
                        product.SoldOut = oProduct.SoldOut;

                        unAvailableProducts.Add(product);

                    }
                }

                OrdersModel OrderDetails = new OrdersModel();
                if (unAvailableProducts.Count() == 0)
                {
                    oOrder.TotalAmount = (oOrder.SubAmount - oModel.DiscountAmount) + oModel.ServiceCharge;

                    db.MW_Orders.Add(oOrder);
                    db.SaveChanges();
                    lstOrdersDetails.ForEach(x => x.OrderID = oOrder.ID);
                    db.MW_OrdersDetails.AddRange(lstOrdersDetails);
                    db.SaveChanges();

                    OrderDetails = GetOrderDetails(oOrder.ID, LanguageID);
                }
                else
                {
                    OrderDetails.lstUnAvailableProducts = unAvailableProducts;
                }

                return OrderDetails;
            }
        }

        public OrdersModel ChangeOrderStatus(long OrderID, int OrderStatusID, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Orders oOrder = db.MW_Orders.FirstOrDefault(x => x.ID == OrderID);
                if (oOrder != null)
                {
                    oOrder.OrderStatusID = OrderStatusID;
                    db.SaveChanges();

                    OrdersModel OrderDetails = GetOrderDetails(oOrder.ID, LanguageID);

                    if (OrderStatusID == 3)
                    {
                        foreach (var item in OrderDetails.lstOrderDetails)
                        {
                            MW_Products oProduct = db.MW_Products.FirstOrDefault(x => x.ID == item.ProductID);
                            if (oProduct != null)
                            {
                                oProduct.Stock += item.Qty;
                                db.SaveChanges();
                            }
                        }
                    }
                    return OrderDetails;
                }
                else
                {
                    return new OrdersModel();
                }
            }
        }

        public List<OrdersModel> GetMyOrders(long CustomerID, int LanguageID, bool IsPosted = false, int OrderStatus = 0)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                CustomersCOM oCustomersCOM = new CustomersCOM();
                List<OrdersModel> lstOrders = (from order in db.MW_Orders
                                               join orderdetails in db.MW_OrdersDetails on order.ID equals orderdetails.OrderID
                                               join orderstatus in db.MW_OrderStatus on order.OrderStatusID equals orderstatus.OrderStatusID
                                               join product in db.MW_Products on orderdetails.ProductID equals product.ID
                                               where order.CustomerID == CustomerID
                                               && (OrderStatus > 0 ? order.OrderStatusID == OrderStatus : true)
                                               && (!IsPosted ? !order.IsDeleted : true)
                                               select new
                                               {
                                                   OrderID = order.ID,
                                                   CustomerID = order.CustomerID,
                                                   SubAmount = order.SubAmount,
                                                   DiscountAmount = order.DiscountAmount,
                                                   ServiceCharge = order.ServiceCharge,
                                                   TotalAmount = order.TotalAmount,
                                                   OrderNotes = order.OrderNotes,
                                                   OrderStatusID = orderstatus.OrderStatusID,
                                                   OrderStatusName = orderstatus.OrderStatusName,
                                                   OrderDetailsID = orderdetails.ID,
                                                   ProductID = orderdetails.ProductID,
                                                   Qty = orderdetails.Qty,
                                                   Price = orderdetails.Price,
                                                   TotalPrice = orderdetails.TotalPrice,
                                                   ProductName = product.ProductName,
                                                   ProductDescription = product.ProductDescription,
                                                   MainImage = product.MainImage,
                                                   CreatedDate = order.CreatedDate,
                                               }).AsEnumerable().GroupBy(x => x.OrderID).Select(order => new OrdersModel()
                                               {
                                                   CustomerID = order.FirstOrDefault().CustomerID,
                                                   DiscountAmount = order.FirstOrDefault().DiscountAmount,
                                                   OrderID = order.FirstOrDefault().OrderID,
                                                   OrderNotes = order.FirstOrDefault().OrderNotes,
                                                   OrderStatusID = order.FirstOrDefault().OrderStatusID,
                                                   OrderStatusName = order.FirstOrDefault().OrderStatusName,
                                                   ServiceCharge = order.FirstOrDefault().ServiceCharge,
                                                   SubAmount = order.FirstOrDefault().SubAmount,
                                                   TotalAmount = order.FirstOrDefault().TotalAmount,
                                                   ocustomer = oCustomersCOM.GetCustomerDetails(order.FirstOrDefault().CustomerID, LanguageID),
                                                   CreatedDate = order.FirstOrDefault().CreatedDate,
                                                   lstOrderDetails = (from item in order
                                                                      select new OrdersDetailsModel()
                                                                      {
                                                                          OrderDetailsID = item.OrderDetailsID,
                                                                          Price = item.Price,
                                                                          ProductID = item.ProductID,
                                                                          Qty = item.Qty,
                                                                          TotalPrice = item.TotalPrice,
                                                                          MainImage = item.MainImage,
                                                                          ProductDescription = item.ProductDescription,
                                                                          ProductName = item.ProductName
                                                                      }).ToList()
                                               }).OrderByDescending(x => x.CreatedDate).ToList();
                return lstOrders;
            }
        }
    }
}