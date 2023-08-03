using Amazon;
using Amazon.Runtime;
using Amazon.S3;

namespace Newme.Catalog.Application.Services.Externals.AmazonS3.Models
{
    public class AmazonS3ConfigModel
    {
        public string AwsKeyId { get; set; }     
        public string AwsKeySecret { get; set; }  
        public BasicAWSCredentials AwsCredentials { get; set; }  
        public IAmazonS3 AwsS3Client { get; set; }
        public static string Buket => "newmecatalog";

        public void Create(AwsS3Model model) 
        {
            AwsKeyId = model.KeyId;
            AwsKeySecret = model.KeySecret;
            AwsCredentials = new BasicAWSCredentials(AwsKeyId, AwsKeySecret);
            var configuration = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };
            AwsS3Client = new AmazonS3Client(AwsCredentials, configuration);
        }
    }
}