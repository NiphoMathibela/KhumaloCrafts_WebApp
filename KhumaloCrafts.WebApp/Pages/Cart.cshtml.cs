using BusinessLogicLayer;
using DataAccessLayer.Models;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace KhumaloCrafts.WebApp.Pages
{
    public class CartModel : PageModel
    {
        BusinessLayer businessObj = new BusinessLayer();

        public List<DProducts> cartProducts = new List<DProducts>();

        public string userId = "1";

        public List<CartItems> cartList = new List<CartItems>();
        public void OnGet()
        {
            cartList = businessObj.GetCartProducts(userId);
        }
    }
}
