using Azure;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using System.Web;

namespace BusinessLogicLayer
{
    public class BusinessLayer
    {
        DataLayer dataClass = new DataLayer();

        public void NewUser(string name, string email, string passwordHash, string shippingAddress, string billingAddress)
        {
            dataClass.CreateUser(name, email, passwordHash, shippingAddress, billingAddress);
        }

        public void NewProduct(string productName, string productDescription, string artisanId, string productCategory, string productPrice, int stock, string img)
        {
            dataClass.CreateProduct(productName, productDescription, artisanId, productCategory, productPrice, stock, img);
        }

        public List<DProducts> GetProducts()
        {
            return dataClass.GetProducts();
        }

        //Creating new order
        public void NewOrder(string userId, string orderDate, string orderStatus, string items, string totalPrice, string shippingAddress)
        {
            dataClass.CreateOrder(userId, orderDate, orderStatus, items, totalPrice, shippingAddress);
        }

        //Cart Price
        public decimal CartPrice(string userId)
        {
            return dataClass.GetCartPrice(userId);
        }

        //Get one product
        public DProducts GetOneProduct(int productId)
        {
           return dataClass.GetSingleProduct(productId);
        }

        //Get artisan products
        public List<DProducts> GetArtisanProducts(string artisanId)
        {
            return dataClass.GetArtisanProducts(artisanId);
        }

        public void UpdateProduct(int productId, string productName, string productDescription, string productCategory, decimal productPrice, int stock, string img)
        {
            dataClass.UpdateProduct(productId, productName, productDescription, productCategory, productPrice, stock, img);
        }

        //Delete product from DB
        public void DeleteProduct(string userId, string productId)
        {
            dataClass.DeleteProduct(userId, productId);
        }

        //Features
        public void CartFeatures(int userId, int productId)
        {
            dataClass.CartFeauture(userId, productId);
        }

        //Get items
        public List<DProducts> GetCartItems(int userId)
        {
            return dataClass.GetItems(userId);
        }
    }
}
