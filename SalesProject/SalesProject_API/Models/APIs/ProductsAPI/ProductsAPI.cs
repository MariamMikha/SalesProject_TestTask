using SalesProject.Models.Modules.Customers;
using SalesProject.Models.Modules.Products;
using SalesProject_API.Models.APIBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesProject_API.Models.APIs.ProductsAPI
{
    public class ProductsAPI
    {
        public ProductsResponseAPI GetProducts(ProductsRequestAPI productsRequestAPI)
        {
            ProductsResponseAPI productsResponseAPI = new ProductsResponseAPI();
            ProductsCOM oProductsCOM = new ProductsCOM();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(productsRequestAPI.APIToken);
            int NumberOfRecords = 0;
            productsResponseAPI.lstProducts = oProductsCOM.GetProducts(productsRequestAPI.LanguageID, out NumberOfRecords, productsRequestAPI.PageID, productsRequestAPI.PageSize);
            productsResponseAPI.NumberOfRecords = NumberOfRecords;

            productsResponseAPI.APIStatus = 1;
            productsResponseAPI.APIMessage = "Success";
            return productsResponseAPI;
        }


        public ProductDetailsResponseAPI GetProductDetails(ProductsRequestAPI productsRequestAPI)
        {
            ProductDetailsResponseAPI productDetailsResponseAPI = new ProductDetailsResponseAPI();
            ProductsCOM oProductsCOM = new ProductsCOM();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(productsRequestAPI.APIToken);
            productDetailsResponseAPI.oProduct = oProductsCOM.GetProductDetails(productsRequestAPI.ProductID, productsRequestAPI.LanguageID, (int)CustomerID, true);
            productDetailsResponseAPI.APIStatus = 1;
            productDetailsResponseAPI.APIMessage = "Success";
            return productDetailsResponseAPI;
        }

        public ProductDetailsResponseAPI SaveProduct(ProductsRequestAPI productsRequestAPI)
        {
            ProductDetailsResponseAPI productDetailsResponseAPI = new ProductDetailsResponseAPI();
            ProductsCOM oProductsCOM = new ProductsCOM();
            //if (string.IsNullOrEmpty(productsRequestAPI.oProduct.ProductName))
            //{
            //    productDetailsResponseAPI.APIMessage = "Product Name Required";
            //    productDetailsResponseAPI.APIStatus = -1;
            //}
            //else if(string.IsNullOrEmpty(productsRequestAPI.oProduct.MainImage))
            //{
            //    productDetailsResponseAPI.APIMessage = "Main Image Required";
            //    productDetailsResponseAPI.APIStatus = -1;
            //}
            //else 
            //{

                productDetailsResponseAPI.oProduct = oProductsCOM.SaveProduct(productsRequestAPI.oProduct, productsRequestAPI.LanguageID);
                productDetailsResponseAPI.APIStatus = 1;
                productDetailsResponseAPI.APIMessage = "Success";
            //}

            
            return productDetailsResponseAPI;
        }


        public APIModel DeleteProduct(ProductsRequestAPI productsRequestAPI)
        {
            APIModel oAPIModel = new APIModel();
            ProductsCOM oProductsCOM = new ProductsCOM();
            bool Result = oProductsCOM.DeleteProduct(productsRequestAPI.ProductID);
            if (Result)
            {
                oAPIModel.APIStatus = 1;
                oAPIModel.APIMessage = "Success";
            }
            else
            {
                oAPIModel.APIStatus = -1;
                oAPIModel.APIMessage = "Failed";
            }
            return oAPIModel;
        }
    }

    public class ProductsRequestAPI : APIModel
    {
        public int ProductID { get; set; }
        public ProductsModel oProduct { get; set; }
    }
    public class ProductsResponseAPI : APIModel
    {
        public List<ProductsModel> lstProducts { get; set; }
        public int NumberOfRecords { get; set; }

    }
    public class ProductDetailsResponseAPI : APIModel
    {
        public ProductsModel oProduct { get; set; }
    }
}