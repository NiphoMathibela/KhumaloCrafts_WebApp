using BusinessLogicLayer;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure.Storage.Blobs;

namespace KhumaloCrafts.WebApp.Pages.Product_Pages
{
    public class AddProductModel : PageModel
    {
        //Business layer instance
        public BusinessLayer businessObj = new BusinessLayer();

        public Product newProduct = new Product();

        //Error message for the file
        public string errMessage = "";

        //Success message
        public string successMssg = "";

		public void OnGet()
        {
        }
		public void OnPost() 
        {

			newProduct.ProductName = Request.Form["productName"];
            newProduct.ProductDescription = Request.Form["productDescription"];
            newProduct.ProductCategory = Request.Form["productCategory"];
            newProduct.ProductPrice = Request.Form["productPrice"];
            newProduct.Img = Request.Form["imageUrl"];

            if (string.IsNullOrEmpty(newProduct.Img))
            {
                newProduct.Img = "https://media.istockphoto.com/id/1226328537/vector/image-place-holder-with-a-gray-camera-icon.jpg?s=612x612&w=0&k=20&c=qRydgCNlE44OUSSoz5XadsH7WCkU59-l-dwrvZzhXsI=";
            }

            string stock = Request.Form["stockAmount"];
            int number;

			if (int.TryParse(stock, out number))
			{
				// Conversion successful, use the 'number' variable
				newProduct.Stock = number;
			}

            string artisanId = "1";

                //Send data to the DB
                businessObj.NewProduct(newProduct.ProductName, newProduct.ProductDescription, artisanId, newProduct.ProductCategory, newProduct.ProductPrice, newProduct.Stock, newProduct.Img);
        }
    }
}
