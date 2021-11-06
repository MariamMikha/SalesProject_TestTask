using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_Customers
    {
        [Key]
        public long ID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string PhoneCode { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public int Gender { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}