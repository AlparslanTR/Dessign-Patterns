using System.Drawing;

namespace WebApp.AdapterPattern.Services
{
    public interface IAdvanceImageProcess
    {
        void AddWaterMarkImage(Stream stream, string text, string filePath, Color color, Color outlineColour);
    }
}
