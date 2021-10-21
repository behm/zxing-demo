using System;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace ZXingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ZXing Demo!");

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Encoder = new QRCodeWriter(),
                Options = new EncodingOptions
                {
                    Width = 800,
                    Height = 800,
                    PureBarcode = false,
                    Margin = 10
                }
            };

            var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZXingDemo");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            
            var fileName = $"BarCode_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid()}.png";
            var filePath = Path.Combine(outputFolder, fileName);
            Console.WriteLine($"Writing barcode to '{filePath}'");

            using var bitmap = writer.Write("https://omnitech-inc.com/");
            bitmap.Save(filePath, ImageFormat.Png);
        }
    }
}
