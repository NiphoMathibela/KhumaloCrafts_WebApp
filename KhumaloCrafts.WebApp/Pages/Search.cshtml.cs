using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Azure.Storage.Blobs.Models;
using BusinessLogicLayer;
using CloudinaryDotNet.Actions;
using DataAccessLayer.Models;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Search;
using Newtonsoft.Json;
using System.Text;


namespace KhumaloCrafts.WebApp.Pages
{
    public class SearchModel : PageModel
    {
        //Busines instance
        public BusinessLayer businessObj = new BusinessLayer();

        public List<DProducts> products = new List<DProducts>();

        public List<ProductAzure> searchResultsList = new List<ProductAzure>();

        //Search text
        [BindProperty]
        public string SearchText { get; set; } 

        //AI Search Client

        public async void OnGet()
        {
           
        }

        public void OnPost() 
        {
            try
            {
                string searchText = SearchText;

                var endpoint = "https://khumalosearch.search.windows.net";
                var indexName = "azuresql-productindex";
                var key = "G98l0Vwswqqnp4qTAiU70PxtI4xav2G4l8q8KwCIWjAzSeDz6QYV";

                SearchClient searchClient = new SearchClient(new Uri(endpoint), indexName, new Azure.AzureKeyCredential(key));

                var options = new SearchOptions();

                options.Select.Add("name");
                options.Select.Add("productId");
                options.Select.Add("categoryId");
                options.Select.Add("description");
                options.Select.Add("price");
                options.Select.Add("images");

                options.IncludeTotalCount = true;

                var results = searchClient.Search<ProductAzure>(searchText, options);

                Console.WriteLine($"rESULTS cOUNT: {results.Value.TotalCount}");

                foreach (var item in results.Value.GetResults())
                {
                    Console.WriteLine(item);


                    searchResultsList.Add(new ProductAzure
                    {
                        productId = item.Document.productId,
                        name = item.Document.name,
                        description = item.Document.description,
                        images = item.Document.images,
                        categoryId = item.Document.categoryId,
                        price = item.Document.price,
                    });
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Something went wrong here! {error.Message}");
                throw;
            }
        }
    }
}
