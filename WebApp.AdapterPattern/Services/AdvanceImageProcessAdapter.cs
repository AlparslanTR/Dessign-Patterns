using System.Drawing;

namespace WebApp.AdapterPattern.Services
{
    public class AdvanceImageProcessAdapter : IImageProcess
    {
        private readonly IAdvanceImageProcess _advanceImageProcess;

        public AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess)
        {
            _advanceImageProcess = advanceImageProcess;
        }

        public void AddWatermark(string text, string fileName, Stream imageStream)
        {
            _advanceImageProcess.AddWaterMarkImage(imageStream, text,$"wwwroot/Images/{fileName}",Color.White,Color.Black);
        }
    }
}
