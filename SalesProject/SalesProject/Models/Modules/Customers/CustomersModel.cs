using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesProject.Models.Modules.Customers
{
    public class CustomersModel
    {
        public long CustomerID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string FullName { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string PhoneCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Validations), ErrorMessageResourceName = "RequirdField")]
        public string PhoneNumber { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public int Gender { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }

        public long CartID { get; set; }



        public string DeviceID { get; set; }
        public string APIToken { get; set; }
        public string DeviceToken { get; set; }
        public int DevicePlatform { get; set; }

    }
}