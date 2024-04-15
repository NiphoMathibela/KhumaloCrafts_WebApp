using BusinessLogicLayer;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            string stock = Request.Form["stockAmount"];
            int number;

			if (int.TryParse(stock, out number))
			{
				// Conversion successful, use the 'number' variable
				newProduct.Stock = number;
			}

                //Send dat to the DB
                businessObj.NewProduct(newProduct.ProductName, newProduct.ProductDescription, newProduct.ProductCategory, newProduct.ProductPrice, newProduct.Stock);
        }
    }
}
