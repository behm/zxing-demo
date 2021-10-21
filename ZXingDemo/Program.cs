using System;
using System.Drawing;
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

            var filePath = WriteBarcode("https://omnitech-inc.com/");
            Console.WriteLine($"Barcode was written to: \n'{filePath}'");

            var content = ReadBarcode(filePath);
            Console.WriteLine($"\nBarcode contents: {content}");
        }

        static string WriteBarcode(string content)
        {
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

            using var bitmap = writer.Write(content);
            bitmap.Save(filePath, ImageFormat.Png);

            return filePath;
        }

        static string ReadBarcode(string filePath)
        {
            var content = string.Empty;

            var reader = new BarcodeReader
            {
                //Options = new DecodingOptions
                //{
                //    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                //}
            };

            using var barcodeBitmap = Image.FromFile(filePath) as Bitmap;
            var result = reader.Decode(barcodeBitmap);
            
            Console.WriteLine("\nBarcode Read Result");
            Console.WriteLine($"Format: {result.BarcodeFormat}");
            Console.WriteLine($"Text  : {result.Text}");

            return result.Text;
        }
    }
}
