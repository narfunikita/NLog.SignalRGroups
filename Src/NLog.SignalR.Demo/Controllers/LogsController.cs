using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NLog.SignalR.Demo.Controllers
{
    public class LogsController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GenerateMessage()
        {
            Logger.Log((new Random()).Next(2) == 1 ? LogLevel.Info : LogLevel.Debug, "Generated log message");

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerateError()
        {
            try
            {
                throw new ArgumentNullException("Generated exception");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return Json("");
        }
    }
}