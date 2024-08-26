using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LiveVoting.Server.Controllers;

[Route("api/s3")]
[ApiController]
public class BucketsController : ControllerBase
{
    private readonly IAmazonS3 _amazonS3;

    public BucketsController(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }
    
    [HttpGet("buckets")]
    public async Task<IActionResult> GetAllBucketsAsync()
    {
        var data = await _amazonS3.ListBucketsAsync();
        var buckets = data.Buckets.Select(b => {return b.BucketName;});
        return Ok(buckets);
    }

    [HttpGet("{bucketName}")]
    public async Task<IActionResult> GetBucketItemsAsync(string bucketName)
    {
        try
        {
            var data = await _amazonS3.ListObjectsAsync(bucketName);
            var objects = data.S3Objects.Select(obj => new
            {
                obj.Key,
                obj.Size,
                obj.LastModified
            });

            return Ok(objects);
        }
        catch (AmazonS3Exception ex)
        {
            return BadRequest($"Error listing objects: {ex.Message}");
        }
    }
    
    [HttpGet("buckets/{bucketName}/{key}")]
    public async Task<IActionResult> GetImage(string bucketName, string key)
    {
        try
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = key
            };

            using var response = await _amazonS3.GetObjectAsync(request);
            using var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            return File(bytes, response.Headers.ContentType);
        }
        catch (AmazonS3Exception ex)
        {
            return BadRequest($"Error retrieving image: {ex.Message}");
        }
    }
    
}
