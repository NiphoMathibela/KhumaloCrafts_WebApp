using BusinessLogicLayer;
using DataAccessLayer.Models;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace KhumaloCrafts.WebApp.Pages
{
    public class MyWorkModel : PageModel
    {
        //Busines instance
        public BusinessLayer businessObj = new BusinessLayer();

        public List<DProducts> products = new List<DProducts>();

        public List<DProducts> searchResults = new List<DProducts>();

        public List<ProductAzure> searchResultsList = new List<ProductAzure>();

        //Search text
        public string searchText;

        //AI Search Client
        public SearchClient searchClient = new SearchClient(
            new Uri("https://khumalosearch.search.windows.net"), "azuresql-productindex", new Azure.AzureKeyCredential("G98l0Vwswqqnp4qTAiU70PxtI4xav2G4l8q8KwCIWjAzSeDz6QYV"));

        public void OnGet()
        {

           if(searchText == null)
            {
                products = businessObj.GetProducts();
            }
            else
            {
                try
                {
                    searchText = Request.Query["searchText"];

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

        public async void OnPost()
        {
            
        }
    }
}
