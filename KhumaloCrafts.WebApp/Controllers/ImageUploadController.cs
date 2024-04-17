using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;

public class ImageUploadController : Controller
{
	private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=st10460431blobstorage;AccountKey=NNwBNucjvRScidXgVJrZwuo+3z7vMntjggcqMM64AZSZbra2OUerDngSECSMH20198RaJ1bMLdIU+AStha952w==;EndpointSuffix=core.windows.net";
	private readonly string containerName = "productimages";

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> UploadImage(IFormFile imageFile)
	{
		if (imageFile != null && imageFile.Length > 0)
		{
			// Create a BlobServiceClient object which will be used to create a container client
			BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

			// Create the container and return a container client object
			BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

			// Get a reference to a blob
			BlobClient blobClient = containerClient.GetBlobClient(imageFile.FileName);

			// Upload the file
			using (Stream stream = imageFile.OpenReadStream())
			{
				await blobClient.UploadAsync(stream, true);
			}
		}

		return RedirectToAction("Index");
	}
}
