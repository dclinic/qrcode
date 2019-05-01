using System.Web.Mvc;
using System.Security.Cryptography;
using System;
using eCommerceApp.Models;
using DataMatrix.net;

namespace eCommerceApp.Controllers
{
    public class generateQRController : Controller
    {
        public ActionResult Index()
        {
            string url = "";

            for (int i = 0; i < 50; i++)
            {
                string uniqueid = "22980001" + GetRandom12();
                url = "http://qrscanner.somee.com/product?uid=" + uniqueid;

                string filename = Server.MapPath("~/QR/20171218_1/" + uniqueid + ".jpg");

                DataMatrix datamatrix = new DataMatrix();
                datamatrix.Data = url;
                datamatrix.Encoding = DataMatrixEncoding.ASCII;
                datamatrix.Format = DataMatrixFormat.Format_10X10;
                datamatrix.ProcessTilde = true;
                datamatrix.UOM = UnitOfMeasure.PIXEL;
                datamatrix.ModuleSize = 6;
                datamatrix.LeftMargin = 2;
                datamatrix.RightMargin = 2;
                datamatrix.TopMargin = 2;
                datamatrix.BottomMargin = 2;
                datamatrix.Resolution = 96;
                datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                datamatrix.drawBarcode(filename);
            }

            return View();
        }

        //public long GetRandom12()
        //{
        //    var bytes = new byte[sizeof(Int64)];
        //    RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
        //    Gen.GetBytes(bytes);
        //    long random = BitConverter.ToInt64(bytes, 0);
        //    string pos = random.ToString().Replace("-", "").Substring(0, 12);
        //    return Convert.ToInt64(pos);
        //}
    }
}