using Amazon.S3;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Sharedlayer.Models.Result;
using Sharedlayer.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Sharedlayer.Interfaces.Services
{
    public interface IFileService
    {
        Task<Result<string>> UploadFile(IAmazonS3 s3client, IOptions<S3Settings> s3settings, IBrowserFile file, byte[] filebytes, string DocumentCategory, string FileName);
    }
}
