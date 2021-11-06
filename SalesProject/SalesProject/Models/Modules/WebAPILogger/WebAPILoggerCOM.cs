using SalesProject.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesProject.Models.Modules.WebAPILogger
{
    public class WebAPILoggerCOM
    {
        public void SaveLog(string sLink, string sRequest, string sResponse, string elapsedTime)
        {
            using (MWCoreEntity db = new MWCoreEntity())
            {
                MW_WebAPILogger oLogger = new MW_WebAPILogger();
                oLogger.RequestDateTime = DateTime.Now;
                oLogger.ServiceLink = sLink;
                oLogger.RequestString = sRequest;
                oLogger.ResponseString = sResponse;
                oLogger.ElapsedTime = elapsedTime;
                db.MW_WebAPILogger.Add(oLogger);
                db.SaveChanges();
            }
        }
    }
}