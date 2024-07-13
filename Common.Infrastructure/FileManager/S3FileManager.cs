using Amazon;
using Amazon.Internal;
using Amazon.S3;
using Amazon.S3.Transfer;
using Infrastructure.FileManager.Contracts;

namespace Infrastructure.FileManager;

public class S3FileManager : IFileManager
{
  private readonly string _bucketName;
  private readonly IAmazonS3 _s3Client;

  public S3FileManager(string bucketName, string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
  {
    _bucketName = bucketName;
    _s3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, region);
  }

  public async Task<string> Save(string base64Content, string fileName)
  {
    var fileBytes = Convert.FromBase64String(base64Content);
    using (var stream = new MemoryStream(fileBytes))
    {
      var uploadRequest = new TransferUtilityUploadRequest
      {
        InputStream = stream,
        Key = fileName,
        BucketName = _bucketName,
        ContentType = "application/octet-stream"
      };

      var fileTransferUtility = new TransferUtility(_s3Client);
      await fileTransferUtility.UploadAsync(uploadRequest);
    }
    
    string fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
    return fileUrl;
  }
}