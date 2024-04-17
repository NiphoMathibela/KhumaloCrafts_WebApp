using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;

namespace KhumaloCrafts.WebApp.Pages
{
    public class CheckOutModel : PageModel
    {
        public BusinessLayer businessObj = new BusinessLayer();

        public decimal cartPrice;
        string userId = "1";
        public void OnGet()
        {
            cartPrice = businessObj.CartPrice(userId);
        }
    }
}
