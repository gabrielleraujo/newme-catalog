
using Microsoft.AspNetCore.Http;

namespace Newme.Catalog.Application.Services.AmazonS3.Externals
{
    public interface IAmazonS3Service
    {
        Task<bool> UploadFileAsync(string bucket, string key, IFormFile file);
    }
}