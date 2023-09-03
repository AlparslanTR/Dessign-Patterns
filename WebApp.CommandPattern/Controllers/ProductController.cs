using Base.Main.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.IO.Pipelines;
using WebApp.CommandPattern.Commands;
using WebApp.CommandPattern.Models;

namespace WebApp.CommandPattern.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());      
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _context.Products.ToListAsync();
            FileCreateInvoker fileCreateInvoker = new ();
           
            EFileType fileType = (EFileType)type;

            switch (fileType)
            {
                case EFileType.excel:
                    ExcelFile<Product> excelFile = new(products);
                    fileCreateInvoker.SetCommand(new CreateExcel<Product>(excelFile));
                    break;
                case EFileType.pdf:
                    PdfFile<Product> pdfFile = new(products,HttpContext);
                    fileCreateInvoker.SetCommand(new CreatePdf<Product>(pdfFile));
                    break;
                default:
                    break;
            }
            return fileCreateInvoker.CreateFile();
        }
        public async Task<IActionResult> CreateBoth()
        {
            var products = await _context.Products.ToListAsync();
            ExcelFile<Product> excelFile = new(products);
            PdfFile<Product> pdfFile = new(products, HttpContext);

            FileCreateInvoker fileCreateInvoker = new ();
            fileCreateInvoker.AddCommend(new CreateExcel<Product>(excelFile));
            fileCreateInvoker.AddCommend(new CreatePdf<Product>(pdfFile));

            var filesResult = fileCreateInvoker.CreateFiles();

            using (var zip = new MemoryStream())
            {
                using (var archive = new ZipArchive(zip,ZipArchiveMode.Create))
                {
                    foreach (var item in filesResult)
                    {
                        var fileContent = item as FileContentResult;
                        var zipFile = archive.CreateEntry(fileContent.FileDownloadName);

                        using (var zipEntryStream = zipFile.Open())
                        {
                           await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
                        }

                    }
                }
                return File(zip.ToArray(), "application/zip", "Excel&Pdf.zip");
            }
            
        }
    }
}