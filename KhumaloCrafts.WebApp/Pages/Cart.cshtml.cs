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

        public int userId = 1;

        public List<DProducts> cartList = new List<DProducts>();
        public void OnGet()
        {
            cartList = businessObj.GetCartItems(userId);
        }
    }
}
