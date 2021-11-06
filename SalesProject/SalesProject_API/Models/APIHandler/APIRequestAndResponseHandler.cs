using MawaqaaCodeLibrary;
using SalesProject.Models.Modules.Customers;
using SalesProject.Models.Modules.WebAPILogger;
using SalesProject_API.Models.APIBaseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SalesProject_API.Models.APIHandler
{
    public class APIRequestAndResponseHandler : DelegatingHandler
    {
        List<string> lstApisWithoutAuth { get; set; }
        protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                lstApisWithoutAuth = GetApisWithoutAuth();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                string sAPIKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"].ToString();
                string requestBody = await request.Content.ReadAsStringAsync();
                APIModel oAPIModel = requestBody.DeserializeJsonToObject<APIModel>();
                bool IsValidToken = false;
                if (lstApisWithoutAuth.Any(x => x.ToLower() == request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower()))
                {
                    if (request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower() == "uploadimageforweb" || request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower() == "uploadfile" || request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower() == "knetresponse" || request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower() == "kneterror" || request.RequestUri.Segments[request.RequestUri.Segments.Length - 1].ToLower() == "getimages")
                    {
                        IsValidToken = true;
                    }
                    else
                    {
                        if (oAPIModel.APIKey == sAPIKey)
                        {
                            IsValidToken = true;
                        }
                        else
                        {
                            IsValidToken = false;
                        }
                    }

                }
                else
                {
                    if (request.Headers.Authorization != null && request.Headers.Authorization.Scheme.ToLower() == "bearer")
                    {
                        string sToken = request.Headers.Authorization.Parameter;
                        CustomersCOM oCustomersCOM = new CustomersCOM();
                        oAPIModel.APIToken = sToken;
                        oAPIModel.UserID = (int)oCustomersCOM.GetCustomerIDByToken(sToken);
                        IsValidToken = oAPIModel.UserID > 0;
                    }
                    else
                    {
                        oAPIModel.LanguageID = oAPIModel.LanguageID != 0 ? oAPIModel.LanguageID : 1;
                        oAPIModel.APIStatus = -3;
                        oAPIModel.APIMessage = "NotAuthorized";
                        return request.CreateResponse(HttpStatusCode.OK, oAPIModel);
                    }
                }
                if (IsValidToken)
                {
                    // log request body


                    // let other handlers process the request
                    var result = await base.SendAsync(request, cancellationToken);
                    var responseBody = "";
                    if (result.Content != null)
                    {
                        // once response body is ready, log it
                        responseBody = await result.Content.ReadAsStringAsync();
                    }
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    if (!request.RequestUri.AbsoluteUri.ToLower().Contains("updatedriverlocation"))
                    {
                        //BackgroundJob.Enqueue(() => SaveLog(request.RequestUri.AbsoluteUri, requestBody, responseBody, elapsedTime));
                        WebAPILoggerCOM webAPILoggerCOM = new WebAPILoggerCOM();
                        webAPILoggerCOM.SaveLog(request.RequestUri.AbsoluteUri, requestBody, responseBody, elapsedTime);
                    }
                    return result;
                }
                else
                {
                    oAPIModel.APIStatus = -3;
                    oAPIModel.APIMessage = "you are not authorized to access";
                    return request.CreateResponse(HttpStatusCode.OK, oAPIModel);
                }

            }
            catch (Exception ex)
            {
                APIModel oAPIModel = new APIModel();
                oAPIModel.APIStatus = -1;
                oAPIModel.APIMessage = ex.Message;
                return request.CreateResponse(HttpStatusCode.OK, oAPIModel);
            }

        }


        public List<string> GetApisWithoutAuth()
        {
            List<string> lstApis = new List<string>();
            lstApis.Add("Register");
            lstApis.Add("Login");
            lstApis.Add("DeleteCustomer");
            lstApis.Add("GetCustomerDetails");
            lstApis.Add("UpdateCustomerProfile");
            lstApis.Add("ChangeCustomerPassword");
            lstApis.Add("GetProducts");
            lstApis.Add("GetProductDetails");
            lstApis.Add("SaveProduct");
            lstApis.Add("DeleteProduct");
            lstApis.Add("GetMyOrders");
            lstApis.Add("GetUserOrders");
            lstApis.Add("GetOrderDetails");
            lstApis.Add("SaveOrder");
            lstApis.Add("ChangeOrderStatus");


           
            return lstApis;
        }
    }
}