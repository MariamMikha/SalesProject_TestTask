using MawaqaaCodeLibrary;
using SalesProject.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SalesProject.Models.Modules.Customers
{
    public class CustomersCOM
    {

        #region GetCustomerDetails
        public CustomersModel GetCustomerDetails(long CustomerID, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {

                CustomersModel oCustomer = (from customer in db.MW_Customers
                                            join device in db.MW_CustomerDevices on customer.ID equals device.UserID
                                            where customer.ID == CustomerID
                                            select new CustomersModel()
                                            {
                                                CustomerID = customer.ID,
                                                Email = customer.Email,
                                                FullName = customer.FullName,
                                                PhoneCode = customer.PhoneCode,
                                                PhoneNumber = customer.PhoneNumber,
                                                Status = customer.Status,
                                                Username = customer.Username,
                                                DateOfBirth = customer.DateOfBirth,
                                                Gender = customer.Gender,
                                                APIToken = device.ApiToken,
                                                Image = customer.Image
                                            }).FirstOrDefault();

                return oCustomer;
            }
        }
        #endregion

        #region SaveCustomer
        public CustomersModel SaveCustomer(CustomersModel oModel, int LanguageID)
        {
            try
            {
                using (MWCoreEntity db = new MWCoreEntity())
                {
                    bool IsNew = true;
                    MW_Customers oCustomer = new MW_Customers();
                    if (oModel.CustomerID > 0)
                    {
                        IsNew = false;
                        oCustomer = db.MW_Customers.FirstOrDefault(x => x.ID == oModel.CustomerID);
                    }
                    bool IsEmailExist = (from customer in db.MW_Customers
                                         where oModel.CustomerID > 0 ? customer.Email == oModel.Email && customer.ID != oModel.CustomerID : customer.Email == oModel.Email
                                         select customer.ID).Any();
                    if (IsEmailExist)
                    {
                        return new CustomersModel()
                        {
                            CustomerID = -99
                        };
                    }
                    bool IPhoneExist = (from customer in db.MW_Customers
                                        where oModel.CustomerID > 0 ? customer.PhoneCode == oModel.PhoneCode && customer.PhoneNumber == oModel.PhoneNumber && customer.ID != oModel.CustomerID : customer.PhoneCode == oModel.PhoneCode && customer.PhoneNumber == oModel.PhoneNumber
                                        select customer.ID).Any();
                    if (IPhoneExist)
                    {
                        return new CustomersModel()
                        {
                            CustomerID = -98
                        };
                    }
                    bool IsUsernameExist = (from customer in db.MW_Customers
                                            where oModel.CustomerID > 0 ? customer.Username == oModel.Username && customer.ID != oModel.CustomerID : customer.Username == oModel.Username
                                            select customer.ID).Any();
                    if (IsUsernameExist)
                    {
                        return new CustomersModel()
                        {
                            CustomerID = -97
                        };
                    }
                    if (db.MW_Customers.Any(x => x.Email == oModel.Email || x.PhoneNumber == oModel.PhoneNumber))
                    {
                        IsNew = true;
                        oCustomer = db.MW_Customers.FirstOrDefault(x => x.Email == oModel.Email || x.PhoneNumber == oModel.PhoneNumber);
                    }
                    if (string.IsNullOrEmpty(oModel.Username)&& !string.IsNullOrEmpty(oModel.Username))
                    {
                        oModel.Username = oModel.Email;
                    }
                    oCustomer.Email = oModel.Email;
                    oCustomer.FullName = oModel.FullName;
                    oCustomer.Username = oModel.Username;
                    oCustomer.PhoneCode = oModel.PhoneCode;
                    oCustomer.PhoneNumber = oModel.PhoneNumber;
                    oCustomer.DateOfBirth = oModel.DateOfBirth;
                    oCustomer.Gender = oModel.Gender;
                    oCustomer.Status = oModel.Status;
                    if (oCustomer.ID == 0)
                    {

                        string sPassword = GetMd5Hash(oModel.Password != null ? oModel.Password : "123456");
                        oCustomer.Password = sPassword;
                        db.MW_Customers.Add(oCustomer);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    MW_CustomerDevices oDevice = db.MW_CustomerDevices.FirstOrDefault(x => x.UserID == oCustomer.ID);
                    if (oDevice == null)
                    {
                        oDevice = new MW_CustomerDevices();
                    }
                    oDevice.ApiToken = GenerateAPIToken();
                    oDevice.DeviceID = oModel.DeviceID;
                    oDevice.DevicePlatform = oModel.DevicePlatform;
                    oDevice.DeviceToken = oModel.DeviceToken;
                    oDevice.UserID = oCustomer.ID;
                    if (oDevice.ID == 0)
                    {
                        db.MW_CustomerDevices.Add(oDevice);
                    }
                    db.SaveChanges();
                    return GetCustomerDetails(oCustomer.ID, LanguageID);
                }
            }
            catch (Exception ex)
            {
                return new CustomersModel();
            }
        }
        #endregion

        #region GenerateAPIToken
        public static string GenerateAPIToken()
        {
            var key = new byte[2048];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);
            return apiKey;
        }
        #endregion

        #region RegisterCustomerDevice
        public CustomersModel RegisterCustomerDevice(CustomersModel oCustomersModel, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_CustomerDevices oDevice = db.MW_CustomerDevices.FirstOrDefault(x => x.UserID == oCustomersModel.CustomerID);
                bool isNew = false;
                if (oDevice == null)
                {
                    isNew = true;
                    oDevice = new MW_CustomerDevices();
                }
                oDevice.ApiToken = GenerateAPIToken();
                oDevice.DeviceID = oCustomersModel.DeviceID;
                oDevice.DevicePlatform = oCustomersModel.DevicePlatform;
                oDevice.DeviceToken = oCustomersModel.DeviceToken;
                oDevice.UserID = oCustomersModel.CustomerID;
                if (isNew)
                {
                    db.MW_CustomerDevices.Add(oDevice);
                }
                db.SaveChanges();
                oCustomersModel = GetCustomerDetails(oCustomersModel.CustomerID, LanguageID);
                return oCustomersModel;
            }
        }
        #endregion

        #region Login
        public CustomersModel Login(CustomersModel oModel, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                string sPassword = GetMd5Hash(oModel.Password);

                var hh = db.MW_Customers.ToList();

                MW_Customers oCustomer = db.MW_Customers.FirstOrDefault(x => (x.Email == oModel.Email || x.Username == oModel.Email) && x.Password == sPassword);
                if (oCustomer != null)
                {
                    oModel.CustomerID = oCustomer.ID;
                    oModel = RegisterCustomerDevice(oModel, LanguageID);
                    return oModel;
                }
                else
                {
                    return new CustomersModel() { CustomerID = -1 };
                }
            }
        }
        #endregion

        #region GetMd5Hash
        public string GetMd5Hash(string input)
        {
            return Security.GetMd5Hash(input);
        }
        #endregion

        #region UpdateCustomerProfileImage
        public CustomersModel UpdateCustomerProfileImage(long CustomerID, string Image, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Customers oCustomer = db.MW_Customers.FirstOrDefault(x => x.ID == CustomerID);
                if (oCustomer != null)
                {
                    oCustomer.Image = Image;
                    db.SaveChanges();
                    return GetCustomerDetails(CustomerID, LanguageID);
                }
                else
                {
                    return new CustomersModel();
                }
            }
        }
        #endregion

        #region ChangePassword
        public CustomersModel ChangePassword(long CustomerID, string OldPassword, string NewPassword)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                CustomersModel oCustomersModel = new CustomersModel();
                MW_Customers oCustomer = db.MW_Customers.FirstOrDefault(x => x.ID == CustomerID);
                string sOldPassword = GetMd5Hash(OldPassword);
                if (oCustomer != null && oCustomer.Password == sOldPassword)
                {
                    string sPassword = GetMd5Hash(NewPassword);
                    oCustomer.Password = sPassword;
                    db.SaveChanges();
                    oCustomersModel.CustomerID = oCustomer.ID;
                }
                else
                {
                    oCustomersModel.CustomerID = -99;
                }
                return oCustomersModel;
            }
        }
        #endregion

        #region GetCustomerIDByToken
        public long GetCustomerIDByToken(string Token)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_CustomerDevices oDevice = db.MW_CustomerDevices.FirstOrDefault(x => x.ApiToken == Token);
                if (oDevice != null)
                {
                    return oDevice.UserID;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion

        #region DeleteCustomer
        public bool DeleteCustomer(long CustomerID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Customers oCustomer = db.MW_Customers.FirstOrDefault(x => x.ID == CustomerID);
                if (oCustomer != null)
                {
                    oCustomer.IsDeleted = true;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region UpdateCustomerProfile
        public CustomersModel UpdateCustomerProfile(CustomersModel oModel, int LanguageID)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_Customers customerEmail = db.MW_Customers.FirstOrDefault(x => x.Email == oModel.Email && x.ID != oModel.CustomerID);
                if (customerEmail != null)
                    return new CustomersModel() { CustomerID = -99 };

                MW_Customers customerMobile = db.MW_Customers.FirstOrDefault(x => x.PhoneCode == oModel.PhoneCode && x.PhoneNumber == oModel.PhoneNumber && x.ID != oModel.CustomerID);
                if (customerMobile != null)
                    return new CustomersModel() { CustomerID = -98 };

                MW_Customers oCustomer = db.MW_Customers.FirstOrDefault(x => x.ID == oModel.CustomerID);
                oCustomer.DateOfBirth = oModel.DateOfBirth;
                oCustomer.FullName = oModel.FullName;
                oCustomer.Gender = oModel.Gender;
                oCustomer.Email = oModel.Email;
                oCustomer.PhoneCode = oModel.PhoneCode;
                oCustomer.PhoneNumber = oModel.PhoneNumber;
                db.SaveChanges();
                return GetCustomerDetails(oCustomer.ID, LanguageID);
            }
        }
        #endregion

        #region CheckEmail
        public bool CheckEmail(CustomersModel oCustomer)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                if (oCustomer.CustomerID == 0)
                {
                    return !db.MW_Customers.Any(x => x.Email == oCustomer.Email);
                }
                else
                {
                    return !db.MW_Customers.Any(x => x.Email == oCustomer.Email && x.ID != oCustomer.CustomerID);
                }
            }
        }
        #endregion

        #region CheckUsername
        public bool CheckUsername(CustomersModel oCustomer)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                if (oCustomer.CustomerID == 0)
                {
                    return !db.MW_Customers.Any(x => x.Username == oCustomer.Username);
                }
                else
                {
                    return !db.MW_Customers.Any(x => x.Username == oCustomer.Username && x.ID != oCustomer.CustomerID);
                }
            }
        }
        #endregion

        #region CheckPhoneNumber
        public bool CheckPhoneNumber(CustomersModel oCustomer)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                if (oCustomer.CustomerID == 0)
                {
                    return !db.MW_Customers.Any(x => x.PhoneCode == oCustomer.PhoneCode && x.PhoneNumber == oCustomer.PhoneNumber);
                }
                else
                {
                    return !db.MW_Customers.Any(x => x.PhoneCode == oCustomer.PhoneCode && x.PhoneNumber == oCustomer.PhoneNumber && x.ID != oCustomer.CustomerID);
                }
            }
        } 
        #endregion
    }
}