using MawaqaaCodeLibrary;
using SalesProject_API.Filters;
using SalesProject_API.Models.APIBaseModel;
using SalesProject_API.Models.APIs.CustomersAPI;
using SalesProject_API.Models.APIs.OrdersAPI;
using SalesProject_API.Models.APIs.ProductsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;

namespace SalesProject_API.Controllers
{
    [ExceptionHandleFilterAttribute]
    public class ServicesController : ApiController
    {

        protected internal bool TryValidateModel(object model)
        {
            return TryValidateModel(model, null /* prefix */);
        }

        protected internal bool TryValidateModel(object model, string prefix)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
            var t = new ModelBindingExecutionContext(new HttpContextWrapper(HttpContext.Current), new System.Web.ModelBinding.ModelStateDictionary());

            foreach (ModelValidationResult validationResult in ModelValidator.GetModelValidator(metadata, t).Validate(null))
            {
                ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
            }

            return ModelState.IsValid;
        }

        #region Customer API
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("Register")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Register(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            if (ModelState.IsValid && TryValidateModel(oCustomersRequestAPI.oCustomer))
            {
                oCustomersResponseAPI = oCustomersAPI.Register(oCustomersRequestAPI);
            }
            else
            {
                oCustomersResponseAPI.APIMessage = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                oCustomersResponseAPI.APIStatus = -1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("Login")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Login(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            if (!string.IsNullOrEmpty(oCustomersRequestAPI.oCustomer.Email) && !string.IsNullOrEmpty(oCustomersRequestAPI.oCustomer.Password))
            {
                oCustomersResponseAPI = oCustomersAPI.Login(oCustomersRequestAPI);
            }
            else
            {
                oCustomersResponseAPI.APIMessage = "Email and Password are required";
                oCustomersResponseAPI.APIStatus = -1;
            }
            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetCustomerDetails")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetCustomerDetails(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = oCustomersAPI.GetCustomerDetails(oCustomersRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("UpdateCustomerProfile")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateCustomerProfile(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            if (ModelState.IsValid && TryValidateModel(oCustomersRequestAPI.oCustomer))
            {
                oCustomersResponseAPI = oCustomersAPI.UpdateCustomerProfile(oCustomersRequestAPI);
            }
            else
            {
                oCustomersResponseAPI.APIMessage = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                oCustomersResponseAPI.APIStatus = -1;
            }
            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("UpdateCustomerProfileImage")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateCustomerProfileImage(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = oCustomersAPI.UpdateCustomerProfileImage(oCustomersRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("ChangePassword")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            CustomersResponseAPI oCustomersResponseAPI = oCustomersAPI.ChangePassword(oCustomersRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oCustomersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("DeleteCustomer")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteCustomer(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            CustomersRequestAPI oCustomersRequestAPI = requestBody.DeserializeJsonToObject<CustomersRequestAPI>();
            oCustomersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            CustomersAPI oCustomersAPI = new CustomersAPI();
            APIModel oAPIModel = oCustomersAPI.DeleteCustomer(oCustomersRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oAPIModel);
        }

        #endregion

        #region Products API
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetProducts")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetProducts(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            ProductsRequestAPI oProductsRequestAPI = requestBody.DeserializeJsonToObject<ProductsRequestAPI>();
            oProductsRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            ProductsAPI oProductsAPI = new ProductsAPI();
            ProductsResponseAPI oProductsResponseAPI = oProductsAPI.GetProducts(oProductsRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oProductsResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("SaveProduct")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SaveProduct(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            ProductsRequestAPI oProductsRequestAPI = requestBody.DeserializeJsonToObject<ProductsRequestAPI>();
            oProductsRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            ProductsAPI oProductsAPI = new ProductsAPI();
            ProductDetailsResponseAPI oProductDetailsResponseAPI = new ProductDetailsResponseAPI();
            if (ModelState.IsValid && TryValidateModel(oProductsRequestAPI.oProduct))
            {
                oProductDetailsResponseAPI = oProductsAPI.SaveProduct(oProductsRequestAPI);

            }
            else
            {
                oProductDetailsResponseAPI.APIMessage = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                oProductDetailsResponseAPI.APIStatus = -1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, oProductDetailsResponseAPI);
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetProductDetails")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetProductDetails(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            ProductsRequestAPI oProductsRequestAPI = requestBody.DeserializeJsonToObject<ProductsRequestAPI>();
            oProductsRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            ProductsAPI oProductsAPI = new ProductsAPI();
            ProductDetailsResponseAPI oProductDetailsResponseAPI = oProductsAPI.GetProductDetails(oProductsRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oProductDetailsResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("DeleteProduct")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteProduct(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            ProductsRequestAPI oProductsRequestAPI = requestBody.DeserializeJsonToObject<ProductsRequestAPI>();
            oProductsRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            ProductsAPI oProductsAPI = new ProductsAPI();
            APIModel oAPIModel = oProductsAPI.DeleteProduct(oProductsRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oAPIModel);
        }
        #endregion

        #region Orders API
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetMyOrders")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetMyOrders(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            OrdersRequestAPI oOrdersRequestAPI = requestBody.DeserializeJsonToObject<OrdersRequestAPI>();
            oOrdersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            OrdersAPI oOrdersAPI = new OrdersAPI();
            OrdersResponseAPI oOrdersResponseAPI = oOrdersAPI.GetMyOrders(oOrdersRequestAPI);
            return Request.CreateResponse(HttpStatusCode.OK, oOrdersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetUserOrders")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetUserOrders(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            OrdersRequestAPI oOrdersRequestAPI = requestBody.DeserializeJsonToObject<OrdersRequestAPI>();
            oOrdersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            OrdersAPI oOrdersAPI = new OrdersAPI();
            OrdersResponseAPI oOrdersResponseAPI = new OrdersResponseAPI();
            if (oOrdersRequestAPI.CustomerID != 0)
            {
                oOrdersResponseAPI = oOrdersAPI.GetUserOrders(oOrdersRequestAPI);
            }
            else
            {
                oOrdersResponseAPI.APIMessage = "CustomerID is required";
                oOrdersResponseAPI.APIStatus = -1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, oOrdersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("GetOrderDetails")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> GetOrderDetails(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            OrdersRequestAPI oOrdersRequestAPI = requestBody.DeserializeJsonToObject<OrdersRequestAPI>();
            oOrdersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            OrdersAPI oOrdersAPI = new OrdersAPI();
            OrdersResponseAPI oOrdersResponseAPI = new OrdersResponseAPI();
            if (oOrdersRequestAPI.OrderID != 0)
            {
                oOrdersResponseAPI = oOrdersAPI.GetOrderDetails(oOrdersRequestAPI);
            }
            else
            {
                oOrdersResponseAPI.APIMessage = "OrderID is required";
                oOrdersResponseAPI.APIStatus = -1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, oOrdersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("SaveOrder")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> SaveOrder(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            OrdersRequestAPI oOrdersRequestAPI = requestBody.DeserializeJsonToObject<OrdersRequestAPI>();
            oOrdersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            OrdersAPI oOrdersAPI = new OrdersAPI();
            OrdersResponseAPI oOrdersResponseAPI = new OrdersResponseAPI();
            if (oOrdersRequestAPI.oOrdersModel != null && oOrdersRequestAPI.oOrdersModel.lstOrderDetails.Count() > 0)
            {
                if (ModelState.IsValid && TryValidateModel(oOrdersRequestAPI.oOrdersModel))
                {
                    oOrdersResponseAPI = oOrdersAPI.SaveOrder(oOrdersRequestAPI);
                }
                else
                {
                    oOrdersResponseAPI.APIMessage = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                    oOrdersResponseAPI.APIStatus = -1;
                }
            }
            else
            {
                oOrdersResponseAPI.APIMessage = "Please enter order";
                oOrdersResponseAPI.APIStatus = -1;
            }


            return Request.CreateResponse(HttpStatusCode.OK, oOrdersResponseAPI);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [ActionName("ChangeOrderStatus")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> ChangeOrderStatus(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            OrdersRequestAPI oOrdersRequestAPI = requestBody.DeserializeJsonToObject<OrdersRequestAPI>();
            oOrdersRequestAPI.APIToken = request.Headers.Authorization != null ? request.Headers.Authorization.Parameter : "";
            OrdersAPI oOrdersAPI = new OrdersAPI();
            OrdersResponseAPI oOrdersResponseAPI = new OrdersResponseAPI();
            if (oOrdersRequestAPI.OrderID != 0 && oOrdersRequestAPI.OrderStatusID != 0)
            {
                oOrdersResponseAPI = oOrdersAPI.ChangeOrderStatus(oOrdersRequestAPI);
            }
            else
            {
                oOrdersResponseAPI.APIMessage = "CustomerID is required";
                oOrdersResponseAPI.APIStatus = -1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, oOrdersResponseAPI);
        }
        #endregion
    }
}
