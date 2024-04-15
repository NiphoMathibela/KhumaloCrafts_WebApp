using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;
using KhumaloCrafts.WebApp.Models;

namespace KhumaloCrafts.WebApp.Pages
{
    public class SignUpModel : PageModel
    {
        //Business Layer Object
        BusinessLayer businessObj = new BusinessLayer();

        public User userInfo = new();

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            userInfo.Name = Request.Form["registerName"];
            userInfo.Email = Request.Form["registerEmail"];
            userInfo.PasswordHash = Request.Form["registerPassword"];
            userInfo.ShippingAddress = Request.Form["shippingAddress"];
            userInfo.BillingAddress = Request.Form["billingAddress"];

            Console.WriteLine(userInfo.Name);

            businessObj.NewUser(userInfo.Name, userInfo.Email, userInfo.PasswordHash, userInfo.ShippingAddress, userInfo.BillingAddress);
        }
    }

}
