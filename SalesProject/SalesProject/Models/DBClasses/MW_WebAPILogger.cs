using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MW_WebAPILogger
    {
        [Key]
        public long ID { get; set; }
        public DateTime RequestDateTime { get; set; }
        public string ServiceLink { get; set; }
        public string RequestString { get; set; }
        public string ResponseString { get; set; }
        public string ElapsedTime { get; set; }
    }
}