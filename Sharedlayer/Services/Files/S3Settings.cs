using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharedlayer.Services.Files
{
    public sealed class S3Settings
    {
        public string Region { get; set; } = string.Empty;
        public string BucketName { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
