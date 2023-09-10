using LazZiya.ImageResize;
using System.Drawing;

namespace WebApp.AdapterPattern.Services
{
    public class AdvanceImageProcess : IAdvanceImageProcess
    {
        public void AddWaterMarkImage(Stream stream, string text, string filePath, Color color, Color outlineColour)
        {
            // Laz ziyanın kütüphanesi ile watermark ekleme işlemi.
            // https://github.com/LazZiya
            using (var img = Image.FromStream(stream))
            {
                var t0ps = new TextWatermarkOptions
                {
                    // Metin rengini ve opaklığını değiştir
                    // Metin opaklığı, Renk'in Alfa kanalına bağlıdır (0 - 255)
                    TextColor = color,

                    // Metin çevresi eklemek
                    // Çevre renginin opaklığı, Rengin alfa kanalına bağlıdır (0 - 255)
                    OutlineColor = outlineColour,

                    // Metni Sağ alta yasla.
                    Location = TargetSpot.BottomRight
                };
                img.AddTextWatermark(text, t0ps).SaveAs(filePath);
            }
        }
    }
}
