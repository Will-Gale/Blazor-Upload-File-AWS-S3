using Amazon.S3;
using Datalayer.Models;
using Microsoft.Extensions.Options;
using Sharedlayer.Interfaces.Services;
using Sharedlayer.Models.Documents;
using Sharedlayer.Models.Result;
using Sharedlayer.Services.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Managers
{
    public class DocumentManager
    {

        private readonly ApplicationDbContext db;

        private readonly IFileService _fileService;
        private readonly IAmazonS3 _s3Client;
        private readonly IOptions<S3Settings> _s3Settings;

        public DocumentManager(ApplicationDbContext context, IFileService fileService, IAmazonS3 s3Client, IOptions<S3Settings> s3Settings)
        {
            db = context;
            _fileService = fileService;
            _s3Client = s3Client;
            _s3Settings = s3Settings;
        }

        public async Task<Result<DocumentsDTO>> SaveDocuments(DocumentsDTO docs)
        {

            if (docs is null || docs.File1 is null)
            {
                return Result<DocumentsDTO>.Failure("Documents cannot be empty.");
            }
            try
            {

                var success1 = await _fileService.UploadFile(_s3Client, _s3Settings, docs.File1, docs.File1Bytes, "Mortgage Product", "Lender Package");
                var success2 = await _fileService.UploadFile(_s3Client, _s3Settings, docs.File2, docs.File2Bytes, "Mortgage Product", "Appraisal");
                var success3 = await _fileService.UploadFile(_s3Client, _s3Settings, docs.File3, docs.File3Bytes, "Mortgage Product", "Purview");
                var success4 = await _fileService.UploadFile(_s3Client, _s3Settings, docs.File4, docs.File4Bytes, "Mortgage Product", "Notice of Assessment");

                if (success1.IsSuccess && success2.IsSuccess && success3.IsSuccess && success4.IsSuccess)
                {

                    var f1 = await RecordDocuments(success1.Data, "Mortgage Product", "Lender Package");
                    var f2 = await RecordDocuments(success2.Data, "Mortgage Product", "Appraisal");
                    var f3 = await RecordDocuments(success3.Data, "Mortgage Product", "Purview");
                    var f4 = await RecordDocuments(success4.Data, "Mortgage Product", "Notice of Assessment");

                    return Result<DocumentsDTO>.Success(docs);
                }

                return Result<DocumentsDTO>.Failure("Product ID cannot be null or empty.");

            }
            catch (Exception ex)
            {
                // Log exception for debugging (uncomment if you have a logging framework)
                // Log.Error("Failed to save MP documents1 info", ex);
                return Result<DocumentsDTO>.Failure("Failed to save MP documents.");
            }

        }

        public async Task<Result<Documents>> RecordDocuments(string key, string documentcategory, string filename)
        {

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(key))
            {
                return Result<Documents>.Failure("Product ID cannot be null or empty.");
            }

            Documents save = new Documents()
            {
                isS3Bucket = true,
                isLocalStorage = false,
                DocumentCategory = documentcategory,
                FileName = filename,
                FileURL = key,
                CreatedUser = "Underwriter",
                DateUploaded = DateTime.UtcNow,
                IsArchived = false,
                WhoArchived = string.Empty,
            };

            try
            {
                await db.MP_Documents.AddAsync(save);
                await db.SaveChangesAsync();

                return Result<Documents>.Success(save);

            }
            catch (Exception ex)
            {
                // Log exception for debugging (uncomment if you have a logging framework)
                // Log.Error("Failed to save MP documents1 info", ex);
                return Result<Documents>.Failure("Failed to record MP documents in SQL.");
            }
        }

    }
}
