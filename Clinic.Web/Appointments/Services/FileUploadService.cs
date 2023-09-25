using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Clinic.Controllers;
using Clinic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clinic.Web.Appointments.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<FileUploadService> logger;

        public FileUploadService(IWebHostEnvironment env,
            ILogger<FileUploadService> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<IList<UploadResult>>> PostFile([FromForm] IEnumerable<IFormFile> files)
        {
            var maxAllowedFiles = 1;
            long maxFileSize = 1024 * 1024 * 15;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new();
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var uploadPath = Path.Combine(exePath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        logger.LogInformation("{FileName} length is 0 (Err: 1)", trustedFileNameForDisplay);
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        logger.LogInformation("{FileName} of {Length} bytes is " +
                            "larger than the limit of {Limit} bytes (Err: 2)",
                            trustedFileNameForDisplay, file.Length, maxFileSize);
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Path.GetRandomFileName();
                            trustedFileNameForFileStorage = Path.ChangeExtension(trustedFileNameForFileStorage, Path.GetExtension(untrustedFileName));
                            var path = Path.Combine(uploadPath, trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            logger.LogInformation("{FileName} saved at {Path}", trustedFileNameForDisplay, path);
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                        }
                        catch (IOException ex)
                        {
                            logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                                trustedFileNameForDisplay, ex.Message);
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    logger.LogInformation("{FileName} not uploaded because the " +
                        "request exceeded the allowed {Count} of files (Err: 4)",
                        trustedFileNameForDisplay, maxAllowedFiles);
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadResults);
        }

        [Route("{file}")]
        [HttpGet]
        [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile(string file)
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var uploadPath = Path.Combine(exePath, "uploads");

            //var FileDirectory = Path.Combine("Files", folder);
            //var RootDirectory = Path.Combine(appRoot, FileDirectory);

            //var FileDirectory = $"\\Files\\{folder}\\";
            //var RootDirectory = appRoot + FileDirectory;

            var filePath = Path.Combine(uploadPath, file);
            if (!File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;

                //if (string.IsNullOrWhiteSpace(downloadModel.ContentType))
                //    return BadRequest(new ArgumentException("File Doesn't Exist."));

                return File(memory, GetContentType(filePath));
            }
        }
        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".webm", "video/webm"},
                {".3gp", "video/3gpp"},
                {".avi", "video/x-msvideo"},
                {".mpeg", "video/mpeg"}
            };
        }

    }
}
