using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_CustomerDevices
    {
        [Key]
        public int ID { get; set; }
        public long UserID { get; set; }
        public string ApiToken { get; set; }
        public string DeviceID { get; set; }
        public string DeviceToken { get; set; }
        public int DevicePlatform { get; set; }
    }
}