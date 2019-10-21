using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Helper
{
    public class ControllerMessage
    {
        public static void Error(Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["messageType"] = "error";
            controller.TempData["messageIcon"] = "thumbs down";
        }
        public static void Success(Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["messageType"] = "success";
            controller.TempData["messageIcon"] = "thumbs up";
        }
        public static void Info(Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["messageType"] = "info";
            controller.TempData["messageIcon"] = "info";
        }
        public static void Warning(Controller controller, string message)
        {
            controller.TempData["message"] = message;
            controller.TempData["messageType"] = "warning";
            controller.TempData["messageIcon"] = "warning";
        }
    }
}