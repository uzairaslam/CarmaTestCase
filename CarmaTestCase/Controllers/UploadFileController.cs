using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LinqToExcel;
using System.Data.OleDb;
using OfficeOpenXml;
using CarmaTestCase.Extensions;
using CarmaTestCase.Models;
using CarmaTestCase.Dtos;
using System.Data;

namespace CarmaTestCase.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationContext _context;
        public UploadFileController(IWebHostEnvironment webHostEnvironment, ApplicationContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FileUpload")]
        public IActionResult UploadExcel(IFormFile excelfile)
        {
            PersonDto person = new PersonDto();
            List<string> columnsName = new List<string>();
            if (excelfile == null)
                return BadRequest();

            if (excelfile.ContentType == "application/vnd.ms-excel" || excelfile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                //var ext = Path.GetExtension(excelfile.FileName);
                //var fil = Path.GetFileNameWithoutExtension(excelfile.FileName);
                string filename = Path.GetFileNameWithoutExtension(excelfile.FileName) + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + Path.GetExtension(excelfile.FileName);
                string targetpath = Path.Combine(_webHostEnvironment.WebRootPath, "Docs/");//Server.MapPath("~/Docs/");

                if (!Directory.Exists(targetpath))
                    Directory.CreateDirectory(targetpath);

                var filePath = Path.Combine(targetpath, filename);
                person.FileName = filePath;

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    excelfile.CopyTo(fileStream);
                }


                //var connectionString = "";
                //if (filename.EndsWith(".xls"))
                //{
                //    connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
                //}
                //else if (filename.EndsWith(".xlsx"))
                //{
                //    connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
                //}
                FileInfo existingFile = new FileInfo(filePath);
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    person.ColumnNames = package.Workbook.Worksheets[0].GetHeaderColumns().StringArrayToSelectListItem();
                }

            }
            else
                return BadRequest();

            //long size = file.Sum(f => f.Length);

            //var filePaths = new List<string>();
            //foreach (var formFile in file)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        // full path to file in temp location
            //        var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
            //        filePaths.Add(filePath);

            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            formFile.CopyTo(stream);
            //        }
            //    }
            //}

            //// process uploaded files
            //// Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = excelfile.Count, size, filePaths });
            return View(person);
        }

        [HttpPost]
        public IActionResult ColumnsMappping(PersonDto dto)
        {
            DataTable dt = new DataTable();
            using (var package = new ExcelPackage(new FileInfo(dto.FileName)))
            {
                dt = package.ToDataTable();
            }

            foreach (DataRow row in dt.Rows)
            {
                Person person = new Person
                {
                    Id_document = dto.Id_document != null ? row[dto.Id_document].ToString() : null,
                    phone = dto.phone != null ? row[dto.phone].ToString() : null,
                    alternative_id = dto.alternative_id != null ? row[dto.alternative_id].ToString() : null,
                    driving_license = dto.driving_license != null ? row[dto.driving_license].ToString() : null,
                    first_name = dto.first_name != null ? row[dto.first_name].ToString() : null,
                    last_name = dto.last_name != null ? row[dto.last_name].ToString() : null,
                    sex = dto.sex != null ? row[dto.sex].ToString() : null,
                    education = dto.education != null ? row[dto.education].ToString() : null,
                    marital_status = dto.marital_status != null ? row[dto.marital_status].ToString() : null,
                    children = dto.children != null ? row[dto.children].ToString() : null,

                };
                _context.Persons.Add(person);
            }
            _context.SaveChanges();

            //return Ok(new { dto});
            return Content("File Saved Successfully");
        }
    }
}