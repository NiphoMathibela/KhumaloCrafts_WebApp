using BusinessLogicLayer;
using DataAccessLayer.Models;
using KhumaloCrafts.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCrafts.WebApp.Pages
{
    public class MyWorkModel : PageModel
    {
        //Busines instance
        public BusinessLayer businessObj = new BusinessLayer();

        public List<DProducts> products = new List<DProducts>();

        public void OnGet()
        {
            products = businessObj.GetProducts();
        }

        public void OnPost()
        {
        }
    }
}
