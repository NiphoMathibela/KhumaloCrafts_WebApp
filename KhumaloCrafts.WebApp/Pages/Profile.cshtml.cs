using BusinessLogicLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCrafts.WebApp.Pages
{
    public class ProfileModel : PageModel
    {
        BusinessLayer businessObj = new BusinessLayer();

        public List<DProducts> forSaleProducts = new List<DProducts>();

        string artisanId = "1";
        public void OnGet()
        {
            forSaleProducts = businessObj.GetArtisanProducts(artisanId);
        }
    }
}
