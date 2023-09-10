using System.Drawing;

namespace WebApp.AdapterPattern.Services
{
    public class ImageProcess : IImageProcess
    {
        public void AddWatermark(string text, string fileName, Stream imageStream)
        {
            // Görüntüyü akıştan al
            using var img = Image.FromStream(imageStream);

            // Görüntü üzerinde çalışmak için bir grafik nesnesi oluştur
            using var graphic = Graphics.FromImage(img);

            // Metin için bir yazı tipi oluştur
            var font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);

            // Metnin boyutunu ölç
            var textSize = graphic.MeasureString(text, font);

            // Su damlasının rengini belirle
            var color = Color.White;

            // Metni boyamak için bir doldurma fırçası oluştur
            var brush = new SolidBrush(color);

            // Metni görüntü üzerine yerleştirme pozisyonunu hesapla
            var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

            // Metni görüntüye ekle
            graphic.DrawString(text, font, brush, position);

            // Görüntüyü belirtilen dosya yoluna kaydet
            img.Save("wwwroot/Images/" + fileName);

            // Kullanılan nesneleri temizle
            img.Dispose();
            graphic.Dispose();
        }
    }
}
