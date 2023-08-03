using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Newme.Catalog.Application.Services.AmazonS3.Externals;
using Newme.Catalog.Application.Services.Externals.AmazonS3.Models;

namespace Newme.Catalog.Application.Services.Externals
{
    public class AmazonS3Service : IAmazonS3Service
    {
        private readonly AmazonS3ConfigModel _config;
        public AmazonS3Service(
            AmazonS3ConfigModel config)
        {
            _config = config;
        }

        public async Task<bool> UploadFileAsync(string bucket, string key, IFormFile file)
        {
            using var newMemoryStream = new MemoryStream();
            await file.CopyToAsync(newMemoryStream);

            var fileTransferUtility = new TransferUtility(_config.AwsS3Client);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = key,
                BucketName = bucket,
                ContentType = file.ContentType
            });

            return true;
        }
    }
}
