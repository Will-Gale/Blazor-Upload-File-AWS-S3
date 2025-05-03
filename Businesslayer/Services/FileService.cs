using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Sharedlayer.Interfaces.Services;
using Sharedlayer.Models.Result;
using Sharedlayer.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services
{
    public class FileService : IFileService
    {

        public async Task<Result<string>> UploadFile(IAmazonS3 s3client, IOptions<S3Settings> s3settings, IBrowserFile file, byte[] filebytes, string DocumentCategory, string FileName)
        {
            if (file.Size == 0 || filebytes.Length == 0)
            {
                return Result<string>.Failure("File cannot be empty...");
            }

            // Copy file to memory stream to avoid Blazor file reference issues
            await using var memoryStream = new MemoryStream(filebytes);
            memoryStream.Position = 0; // Reset position to start

            var key = Guid.NewGuid();

            var putrequest = new PutObjectRequest
            {
                BucketName = s3settings.Value.BucketName,
                Key = $"{DocumentCategory}/{FileName}-{key}",
                InputStream = memoryStream,
                ContentType = file.ContentType,
                Metadata =
                {
                    ["file-name"] = file.Name
                }
            };

            try
            {
                await s3client.PutObjectAsync(putrequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AWS Upload Failed: {ex.Message}");
                return Result<string>.Failure("Upload failed.");
            }

            string returnkey = putrequest.Key;

            return Result<string>.Success(returnkey);
        }

    }
}
