using SalesProject.Models.Modules.Customers;
using SalesProject_API.Models.APIBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesProject_API.Models.APIs.CustomersAPI
{
    public class CustomersAPI
    {
        public CustomersResponseAPI Register(CustomersRequestAPI oCustomersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            oCustomersRequestAPI.oCustomer.Status = true;
            oCustomersResponseAPI.oCustomer = oCustomersCOM.SaveCustomer(oCustomersRequestAPI.oCustomer, oCustomersRequestAPI.LanguageID);

            
            if (oCustomersResponseAPI.oCustomer.CustomerID == -99)
            {
                oCustomersResponseAPI.APIMessage = "Email already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else if (oCustomersResponseAPI.oCustomer.CustomerID == -98)
            {
                oCustomersResponseAPI.APIMessage = "Mobile already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else if (oCustomersResponseAPI.oCustomer.CustomerID == -97)
            {
                oCustomersResponseAPI.APIMessage = "UserName already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else
            {
                oCustomersRequestAPI.oCustomer.CustomerID = oCustomersResponseAPI.oCustomer.CustomerID;
                oCustomersResponseAPI.oCustomer = oCustomersCOM.RegisterCustomerDevice(oCustomersRequestAPI.oCustomer, oCustomersRequestAPI.LanguageID);
                oCustomersResponseAPI.APIMessage = "Registeration successed";
                oCustomersResponseAPI.APIStatus = 1;
            }
            oCustomersResponseAPI.APIKey = oCustomersRequestAPI.APIKey;

            oCustomersResponseAPI.APIToken = oCustomersResponseAPI.oCustomer != null ? oCustomersResponseAPI.oCustomer.APIToken : oCustomersRequestAPI.APIToken;
            oCustomersResponseAPI.LanguageID = oCustomersRequestAPI.LanguageID;

            return oCustomersResponseAPI;
        }

        public CustomersResponseAPI Login(CustomersRequestAPI oCustomersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            oCustomersResponseAPI.oCustomer = oCustomersCOM.Login(oCustomersRequestAPI.oCustomer, oCustomersRequestAPI.LanguageID);

            if (oCustomersResponseAPI.oCustomer != null)
            {
                if (oCustomersResponseAPI.oCustomer.CustomerID == -1)
                {
                    oCustomersResponseAPI.oCustomer = null;
                    oCustomersResponseAPI.APIMessage = "UserName or Password wrong";
                    oCustomersResponseAPI.APIStatus = -1;

                }
                else if (oCustomersResponseAPI.oCustomer.Status == false)
                {
                    oCustomersResponseAPI.oCustomer = null;
                    oCustomersResponseAPI.APIMessage = "Your account is blocked";
                    oCustomersResponseAPI.APIStatus = -1;
                }
                else
                {
                    //if (oCustomersRequestAPI.CartID > 0)
                    //{
                    oCustomersResponseAPI.oCustomer = oCustomersCOM.GetCustomerDetails(oCustomersResponseAPI.oCustomer.CustomerID, oCustomersRequestAPI.LanguageID);
                    //}
                    oCustomersResponseAPI.APIMessage = "Login Success";
                    oCustomersResponseAPI.APIStatus = 1;
                }

            }
            else
            {
                oCustomersResponseAPI.APIMessage = "UserName or Password wrong";
                oCustomersResponseAPI.APIStatus = -1;
            }
            oCustomersResponseAPI.APIKey = oCustomersRequestAPI.APIKey;

            oCustomersResponseAPI.APIToken = oCustomersResponseAPI.oCustomer != null ? oCustomersResponseAPI.oCustomer.APIToken : oCustomersRequestAPI.APIToken;
            oCustomersResponseAPI.LanguageID = oCustomersRequestAPI.LanguageID;
            oCustomersResponseAPI.UserID = oCustomersRequestAPI.UserID;
            return oCustomersResponseAPI;
        }

        public CustomersResponseAPI GetCustomerDetails(CustomersRequestAPI customersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(customersRequestAPI.APIToken);
            customersRequestAPI.oCustomer = new CustomersModel() { CustomerID = CustomerID };
            oCustomersResponseAPI.oCustomer = oCustomersCOM.GetCustomerDetails(customersRequestAPI.oCustomer.CustomerID, customersRequestAPI.LanguageID);
            if (oCustomersResponseAPI.oCustomer != null && oCustomersResponseAPI.oCustomer.CustomerID > 0)
            {
                oCustomersResponseAPI.APIStatus = 1;
                oCustomersResponseAPI.APIMessage = "Success";
            }
            else
            {
                oCustomersResponseAPI.APIStatus = -1;
                oCustomersResponseAPI.APIMessage = "Wrong Customer";
            }
            return oCustomersResponseAPI;
        }

        public CustomersResponseAPI UpdateCustomerProfile(CustomersRequestAPI customersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(customersRequestAPI.APIToken);
            customersRequestAPI.oCustomer.CustomerID = CustomerID;
            customersRequestAPI.oCustomer.Status = true;
            oCustomersResponseAPI.oCustomer = oCustomersCOM.UpdateCustomerProfile(customersRequestAPI.oCustomer, customersRequestAPI.LanguageID);
            if (oCustomersResponseAPI.oCustomer.CustomerID == -99)
            {
                oCustomersResponseAPI.APIMessage = "Email already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else if (oCustomersResponseAPI.oCustomer.CustomerID == -98)
            {
                oCustomersResponseAPI.APIMessage = "Mobile already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else if (oCustomersResponseAPI.oCustomer.CustomerID == -97)
            {
                oCustomersResponseAPI.APIMessage = "UserName already exist";
                oCustomersResponseAPI.APIStatus = -1;
            }
            else if (oCustomersResponseAPI.oCustomer != null && oCustomersResponseAPI.oCustomer.CustomerID > 0)
            {
                oCustomersResponseAPI.APIStatus = 1;
                oCustomersResponseAPI.APIMessage = "Success";
            }
            else
            {
                oCustomersResponseAPI.APIStatus = -1;
                oCustomersResponseAPI.APIMessage = "Wrong Customer";
            }
            return oCustomersResponseAPI;
        }

        public CustomersResponseAPI UpdateCustomerProfileImage(CustomersRequestAPI customersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(customersRequestAPI.APIToken);
            oCustomersResponseAPI.oCustomer = oCustomersCOM.UpdateCustomerProfileImage(CustomerID, customersRequestAPI.oCustomer.Image, customersRequestAPI.LanguageID);
            if (oCustomersResponseAPI.oCustomer != null && oCustomersResponseAPI.oCustomer.CustomerID > 0)
            {
                oCustomersResponseAPI.APIStatus = 1;
                oCustomersResponseAPI.APIMessage = "Success";
            }
            else
            {
                oCustomersResponseAPI.APIStatus = -1;
                oCustomersResponseAPI.APIMessage = "Wrong Customer";
            }
            return oCustomersResponseAPI;
        }

        public CustomersResponseAPI ChangePassword(CustomersRequestAPI customersRequestAPI)
        {
            CustomersResponseAPI oCustomersResponseAPI = new CustomersResponseAPI();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            long CustomerID = oCustomersCOM.GetCustomerIDByToken(customersRequestAPI.APIToken);
            oCustomersResponseAPI.oCustomer = oCustomersCOM.ChangePassword(CustomerID, customersRequestAPI.OldPassword, customersRequestAPI.oCustomer.Password);
            if (oCustomersResponseAPI.oCustomer != null && oCustomersResponseAPI.oCustomer.CustomerID > 0)
            {
                oCustomersResponseAPI.APIStatus = 1;
                oCustomersResponseAPI.APIMessage = "ChangePassword Successed";
            }
            else
            {
                oCustomersResponseAPI.APIStatus = -1;
                oCustomersResponseAPI.APIMessage = "Wrong Old PAssword";
            }
            return oCustomersResponseAPI;
        }

        public APIModel DeleteCustomer(CustomersRequestAPI customersRequestAPI)
        {
            APIModel oAPIModel = new APIModel();
            CustomersCOM oCustomersCOM = new CustomersCOM();
            bool Result = oCustomersCOM.DeleteCustomer(customersRequestAPI.CustomerID);
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

    public class CustomersRequestAPI : APIModel
    {
        public long CustomerID { get; set; }
        public long CartID { get; set; }
        public string OldPassword { get; set; }
        public string Code { get; set; }
        public CustomersModel oCustomer { get; set; }
        public long OrderID { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CustomersResponseAPI : APIModel
    {
        public CustomersModel oCustomer { get; set; }
    }
}