using BusinessLogicLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace KhumaloCrafts.WebApp.Pages
{
    public class EditProductModel : PageModel
    {
        BusinessLayer businessObj = new BusinessLayer();

        public DProducts product = new DProducts();

        public DProducts newProduct = new DProducts();

        public int productId = 1;
		public void OnGet()
        {
			productId = int.Parse(Request.Query["id"]);
            product = businessObj.GetOneProduct(productId);
        }

        public void OnPost()
        {
            newProduct.Id = int.Parse(Request.Form["id"]);
            newProduct.ProductName = Request.Form["productName"];
            newProduct.ProductDescription = Request.Form["productDescription"];
            newProduct.ProductCategory = Request.Form["productCategory"];
            newProduct.ProductPrice = decimal.Parse(Request.Form["productPrice"]);
            newProduct.Img = Request.Form["img"];

            string stock = Request.Form["stockAmount"];
            int number;

            if (int.TryParse(stock, out number))
            {
                // Conversion successful, use the 'number' variable
                newProduct.Stock = number;
            }

            //Send data to the DB
            businessObj.UpdateProduct(newProduct.Id, newProduct.ProductName, newProduct.ProductDescription, newProduct.ProductCategory, newProduct.ProductPrice, newProduct.Stock, newProduct.Img);
            Response.Redirect("/Profile");
        }
    }
}
