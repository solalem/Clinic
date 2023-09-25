using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Clinic.Web.Infrastructure
{
    public class LocalFileService 
    {
        private Dictionary<string, string> _acceptedMimeTypes;
        private string _fileDirectory;

        public LocalFileService(string fileDirectory = "\\Files\\", Dictionary<string, string> acceptedMimeTypes = null)
        {
            _fileDirectory = fileDirectory;
            _acceptedMimeTypes = acceptedMimeTypes;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            // Check for allowed file types
            if (file.Length == 0)
                throw new ArgumentException("Empty file content");
            var extension = Path.GetExtension(file.FileName);
            string[] allowed = { ".jpg", ".png", ".gif", ".pdf", ".doc", ".xls", ".ppt", ".docx", ".xlsx", ".pptx" };
            if (!allowed.Contains(extension))
                throw new ArgumentException("File extension not allowed");

            var uniqueFileName = Guid.NewGuid();
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//.CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            var rootDirectory = appRoot + _fileDirectory;

            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }

            string filePath = @appRoot + _fileDirectory;
            string fileName = uniqueFileName + extension;
            using (var stream = new FileStream($"{filePath}{fileName}", FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<MemoryStream> GetFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Empty file name");
            }

            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            var RootDirectory = appRoot + _fileDirectory;

            var filePath = Path.Combine(RootDirectory, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Image not found");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;//, GetContentType(filePath), Path.GetFileName(filePath));
        }

        public Dictionary<string, string> GetDefaultMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"}
            };
        }
    }
}
