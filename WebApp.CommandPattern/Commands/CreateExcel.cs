using Microsoft.AspNetCore.Mvc;

namespace WebApp.CommandPattern.Commands
{
    public class CreateExcel<T> : ITableActionCommand
    {
        private readonly ExcelFile<T> _excelFile;

        public CreateExcel(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }

        public IActionResult Execute()
        {
            var memoryStream = _excelFile.Create();
            return new FileContentResult(memoryStream.ToArray(), _excelFile.FileType)
            {
                FileDownloadName = _excelFile.FileName,
            };
        }
    }
}
