using Microsoft.AspNetCore.Mvc;

namespace WebApp.CommandPattern.Commands
{
    public class CreatePdf<T> : ITableActionCommand
    {
        private readonly PdfFile<T> _pdfFile;

        public CreatePdf(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            var pdfMemorySteam = _pdfFile.Create();
            return new FileContentResult(pdfMemorySteam.ToArray(), _pdfFile.FileType) 
            {
                FileDownloadName = _pdfFile.FileName,
            };
        }
    }
}
