using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Messageboard.Web.Controllers
{
    public class MessageboardController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}