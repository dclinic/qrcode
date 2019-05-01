using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using eCommerceApp.Models;
using System.Drawing;
using OfficeOpenXml.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Data;

namespace eCommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly eCommerceAppEntities db = new eCommerceAppEntities();

        Image Dataimg;
        DataMatrix.net.DmtxImageEncoder DataEncoder = new DataMatrix.net.DmtxImageEncoder();
        int X, Y;

        public ActionResult Index()
        {
            //string rootPath = "C://RASP//GENERATE_QR//";

            //try
            //{
            //    int skip = 0;

            //    for (int i = 1; i <= 23; i++)
            //    {
            //        string mainPath = rootPath + "//Folder-" + i;
            //        string excelPath = mainPath + "//" + i + ".xlsx";
            //        string qrPath = rootPath + "//Folder-" + i + "//" + i;

            //        if (!Directory.Exists(qrPath))
            //            Directory.CreateDirectory(qrPath);

            //        using (var package = new ExcelPackage(new FileInfo(excelPath)))
            //        {
            //            int r = 1;
            //            string pre = "000";

            //            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("QR CODE SCANNER");
            //            var lstProduct = db.tblProducts.OrderBy(x => x.ID).Skip(skip).Take(2000).ToList();

            //            foreach (var item in lstProduct)
            //            {
            //                worksheet.Cells[r, 1].Value = item.UID.ToString();

            //                string url = "http://ehealthier.eproms.com.au:12345/product?uid=" + item.UID;

            //                if (r == 10)
            //                    pre = "00";
            //                if (r == 100)
            //                    pre = "0";
            //                if (r == 1000)
            //                    pre = "";

            //                DataMatrix.net.DmtxImageEncoder DmtxImageEncoder = new DataMatrix.net.DmtxImageEncoder();
            //                DataMatrix.net.DmtxImageEncoderOptions DataEncodeOptions = new DataMatrix.net.DmtxImageEncoderOptions();
            //                DataEncodeOptions.SizeIdx = DataMatrix.net.DmtxSymbolSize.DmtxSymbolSquareAuto;
            //                DataEncodeOptions.Scheme = DataMatrix.net.DmtxScheme.DmtxSchemeAsciiGS1;
            //                DataEncodeOptions.ModuleSize = 2;
            //                DataEncodeOptions.MarginSize = 2;
            //                DataEncodeOptions.ForeColor = Color.Black;
            //                DataEncodeOptions.BackColor = Color.White;
            //                Dataimg = DataEncoder.EncodeImage(url, DataEncodeOptions);
            //                Bitmap Databitmap = new Bitmap(Dataimg);
            //                Bitmap barcodeImage = Databitmap;
            //                X = Databitmap.Width;
            //                Y = Databitmap.Height;
            //                barcodeImage.Save(qrPath + "//" + pre + "" + r + ".jpg");

            //                r++;
            //            }

            //            worksheet.Column(1).AutoFit();

            //            package.Save();
            //        }

            //        skip = skip + 2000;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return View();
        }

        public ActionResult GenerateSticker()
        {
            List<tblProduct> prodMetaData = new List<tblProduct>();

            prodMetaData = db.tblProducts.ToList();

            return View(prodMetaData);
        }

        public ActionResult GenerateOneSticker()
        {
            List<tblProduct> prodMetaData = new List<tblProduct>();

            prodMetaData = db.tblProducts.Where(x => x.ID == 1).ToList();

            return View(prodMetaData);
        }

        public ActionResult GenerateOneSticker2()
        {
            return View(db.tblProducts.Where(x => x.ID == 1).FirstOrDefault());
        }

        public ActionResult GenerateOneSticker3()
        {
            return View(db.tblProducts.Where(x => x.ID == 1).FirstOrDefault());
        }

        public ActionResult GenerateOneSticker4()
        {
            return View(db.tblProducts.Where(x => x.ID == 1).FirstOrDefault());
        }

        public ActionResult ReadExcelSheet()
        {
            string fname = "~/Content/1.xlsx";

            var package = new ExcelPackage(new FileInfo(Server.MapPath(fname)));
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];

            int totalRows = sheet.Dimension.End.Row;
            int totalCols = sheet.Dimension.End.Column;

            for (int i = 2; i <= totalRows; i++)
            {
                string uid = (string)sheet.Cells[i, 3].Value;
                string sid = uid.Substring(0, 5) + "" + uid.Substring(6, 11);

                string epd = (string)sheet.Cells[i, 4].Value;
                string[] sepd = epd.Split('-');
                int year = Convert.ToInt32(sepd[0]);
                int month = Convert.ToInt32(sepd[1]);
                int day = Convert.ToInt32("01");

                tblProduct prod = new tblProduct();
                prod.ProductName = "RICE";
                prod.Brand = "Victoria";
                prod.Weight = "10 KG";
                prod.Origin = "PRODUCE OF INDIA";
                prod.GTN = (string)sheet.Cells[i, 5].Value;
                prod.UID = uid;
                prod.LOT = (string)sheet.Cells[i, 1].Value;
                prod.Expiry = new DateTime(year, month, day);
                prod.ImportedBy = "GITI NEGIN IRANIAN co.";
                prod.ImageUrl = "/prod/victoria.png";
                prod.ScratchID = sid;
                db.tblProducts.Add(prod);
                db.SaveChanges();
            }

            return View();
        }

        public long GetRandom12()
        {
            var bytes = new byte[sizeof(Int64)];
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();
            Gen.GetBytes(bytes);
            long random = BitConverter.ToInt64(bytes, 0);
            string pos = random.ToString().Replace("-", "").Substring(0, 12);
            return Convert.ToInt64(pos);
        }

        public ActionResult InsertRecord()
        {
            try
            {
                for (int i = 0; i < 41200; i++)
                {
                    string uid = GetRandom12().ToString();
                    string uniqueid = "22980001" + uid;

                    tblProduct prod = new tblProduct();
                    prod.ProductName = "RICE";
                    prod.Brand = "Highly";
                    prod.Weight = "10 KG";
                    prod.Origin = "PRODUCE OF INDIA";
                    prod.GTN = "08908003050997";
                    prod.UID = uniqueid;
                    prod.LOT = "IRH/1005";
                    prod.Expiry = new DateTime(2019, 12, 01);
                    prod.ImportedBy = "TABIAT SABJ MIHAN CO, Tel + 98(0)21 47190000";
                    prod.ImageUrl = "/prod/highly.png";
                    prod.ScratchID = "2298001" + uid.Substring(0, 9);

                    db.tblProducts.Add(prod);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }
    }
}