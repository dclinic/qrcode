using eCommerceApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace eCommerceApp.Controllers
{
    public class productController : Controller
    {
        private readonly eCommerceAppEntities db = new eCommerceAppEntities();

        public ActionResult Index(string uid = "")
        {
            tblProduct prodMetaData = new tblProduct();

            if (Request.QueryString["uid"] != null)
            {
                if (Request.QueryString["uid"].ToString() != "")
                {
                    string uidNew = Request.QueryString["uid"].ToString();
                    prodMetaData = db.tblProducts.Where(x => x.UID == uidNew).FirstOrDefault();
                }
            }

            if (Request.QueryString["sid"] != null)
            {
                if (Request.QueryString["sid"].ToString() != "")
                {
                    string uidNew = Request.QueryString["sid"].ToString();
                    prodMetaData = db.tblProducts.Where(x => x.ScratchID == uidNew).FirstOrDefault();
                }
            }

            if (!string.IsNullOrEmpty(uid))
            {
                prodMetaData = db.tblProducts.Where(x => x.UID == uid).FirstOrDefault();
            }

            return View(prodMetaData);
        }
    }
}