using IronBarCode;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace QRCodeToolbox.Models
{
    public class QRCodeModel
    {
        public string? Text { get; set; }
        public int PixelsPerModule { get; set; } = 70;

        public BitmapImage? GenerateQRCode()
        {
            if (string.IsNullOrWhiteSpace(Text))
                throw new ArgumentException("Text cannot be empty");

            try
            {
                GeneratedBarcode qrCode = QRCodeWriter.CreateQrCode(Text, PixelsPerModule, QRCodeWriter.QrErrorCorrectionLevel.Medium);
                Bitmap qrCodeImage = qrCode.ToBitmap();
                return BitmapToImageSource(qrCodeImage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to generate QR code", ex);
            }
        }

        public void SaveQRCodeToFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(Text))
                throw new ArgumentException("Text cannot be empty");

            try
            {
                GeneratedBarcode qrCode = QRCodeWriter.CreateQrCode(Text, PixelsPerModule, QRCodeWriter.QrErrorCorrectionLevel.Medium);
                qrCode.SaveAsPng(filePath);
                
            } 
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save QR code", ex);
            }
        }

        private static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            // Scale up the bitmap for better quality when copying to clipboard
            int scaleFactor = 3;
            Bitmap scaledBitmap = new Bitmap(bitmap.Width * scaleFactor, bitmap.Height * scaleFactor);
            
            using (Graphics g = Graphics.FromImage(scaledBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap, 0, 0, scaledBitmap.Width, scaledBitmap.Height);
            }

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                scaledBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.DecodePixelWidth = scaledBitmap.Width;
                bitmapImage.DecodePixelHeight = scaledBitmap.Height;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            bitmap.Dispose();
            scaledBitmap.Dispose();
            return bitmapImage;
        }
    }
}
