using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework1_495.Models;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Text;
using QRCoder;
using System.Text.Encodings.Web;
using System.Drawing;
using System.IO;

namespace Homework1_495.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {

            return View(new UploadImageTest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string answer, string InputText, string Color1, string Color2, UploadImageTest model)
        {
            if (ModelState.IsValid && !String.IsNullOrWhiteSpace(answer))
            {
                switch (answer)
                {
                    case "Submit":
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(InputText, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20, Color1, Color2);
                        var bitmapBytes = ConvertToByteArray(qrCodeImage); //Convert image into a byte array
                        return File(bitmapBytes, "image/jpeg"); //Return image

                    case "Upload":
                        var img = model.UploadedImage;
                        

                        var bitmap = new System.Drawing.Bitmap(img.OpenReadStream());
                        

                        QRCodeDecoder dec = new QRCodeDecoder();
                        var text = (dec.Decode(new QRCodeBitmapImage(bitmap)));

                        var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
                        return new FileStreamResult(stream, "text/plain");
                        
                    default:
                        break;
                }
                
            }
            return View();
        }
        
        private static byte[] ConvertToByteArray(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        

        


    }
}
