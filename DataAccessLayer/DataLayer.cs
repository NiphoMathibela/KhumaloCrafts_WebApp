using Microsoft.Data.SqlClient;
using DataAccessLayer.Models;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class DataLayer
    {
        public static string connectionString = @"Server=tcp:st10460431.database.windows.net,1433;Initial Catalog=KhumaloDB;Persist Security Info=False;User ID=nipho;Password=Excellent28@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        SqlConnection conn = new(connectionString);

        SqlCommand? cmd;

        void OpenCloseDatabase()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else
            {
                conn.Close();
            }
        }

        //CREATE NEW USER
        public void CreateUser(string name, string email, string passwordHash, string shippingAddress, string billingAddress)
        {
            OpenCloseDatabase();
            string sql = $"insert into Users(name, email, passwordHash, shippingAddress, billingAddress) values('{name}', '{email}', '{passwordHash}', '{shippingAddress}', '{billingAddress}')";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        //CREATE NEW PRODUCT
        public void CreateProduct(string productName, string productDescription, string artisanId, string productCategory, string productPrice, int stock, string img)
        {
            OpenCloseDatabase();
            string sql = $"insert into Products(name, description, artisanId, categoryId, price, stock, images) values('{productName}', '{productDescription}', '{artisanId}', '{productCategory}', '{productPrice}', '{stock}', '{img}')";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        //GET PRODUCTS
        public List<DProducts> GetProducts()
        {
            OpenCloseDatabase();
            string sql = "select * from Products";
            cmd = new SqlCommand(sql, conn);

            List<DProducts> products = new List<DProducts>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DProducts product = new DProducts();

                    product.Id = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.ProductDescription = reader.GetString(2);
                    product.ProductCategory = reader.GetString(4);
                    product.ProductPrice = reader.GetDecimal(5);
                    product.Stock = reader.GetInt32(6);
                    product.Img = reader.GetString(7);

                    products.Add(product);
                }
            }

            return products;
        }

        //NEW ORDER
        public void CreateOrder(string userId, string orderDate, string orderStatus, string items, string totalPrice, string shippingAddress)
        {
            OpenCloseDatabase();
            string sql = $"insert into Orders(userId, orderDate, orderStatus, items, totalPrice, shippingAddress) values('{userId}', '{orderDate}', '{orderStatus}', '{items}', '{totalPrice}', '{shippingAddress}')";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        //CART ITEMS
        public List<CartItems> GetCartItems(string userId)
        {
            OpenCloseDatabase();
            string sql = $"select * from CartItems where userId = {userId}";
            cmd = new SqlCommand(sql, conn);

            List<CartItems> item = new List<CartItems>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CartItems items = new CartItems();

                    items.OrderId = reader.GetInt32(0);
                    items.Name = reader.GetString(1);
                    items.Price = reader.GetString(2);
                    items.Images = reader.GetString(3);
                    items.UserId = reader.GetString(4);
                    items.ProductId = reader.GetString(5);
                    items.OrderDate = reader.GetDateTime(6);
                    items.OrderStatus = reader.GetString(7);
                    items.Quantity = reader.GetInt32(10);

                    item.Add(items);
                }
            }

            return item;
        }

        public void UpdateProductQuantity(int quantity, string userId, string productId)
        {
            OpenCloseDatabase();

            string sql = $"update CartItems set quantity='{quantity}' where userId = '{userId}' and productId = '{productId}'";

            SqlCommand cmd = new SqlCommand(sql, conn);

            // Execute the SQL command
            cmd.ExecuteNonQuery();
        }

        public void AddCartItems(string name, string price, string images, string userId, string productId, string orderDate, string orderStatus, int quantity)
        {

            OpenCloseDatabase();

            // Prepare SQL command with parameters to avoid SQL injection
            string sql = $"insert into CartItems(name, price, images, userId, productId, orderDate, orderStatus, quantity) values('{name}', '{price}', '{images}', '{userId}', '{productId}', '{orderDate}', '{orderStatus}', '{quantity}')";

            SqlCommand cmd = new SqlCommand(sql, conn);

            // Execute the SQL command
            cmd.ExecuteNonQuery();
        }

        public void UpdateQuantity(int quantity, string userId, string productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // Open the connection
                    string sql = $"UPDATE CartItems SET quantity='{quantity}' WHERE userId = '{userId}' AND productId = '{productId}'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // Execute your query
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    conn.Close();
                }
            }
        }

        public decimal GetCartPrice(string userId)
        {
            OpenCloseDatabase();
            string sql = $"select * from CartItems where userId = {userId}";
            cmd = new SqlCommand(sql, conn);

            List<CartItems> item = new List<CartItems>();

            decimal totalPrice = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CartItems items = new CartItems();

                    items.OrderId = reader.GetInt32(0);
                    items.Name = reader.GetString(1);
                    items.Price = reader.GetString(2);
                    items.Images = reader.GetString(3);
                    items.UserId = reader.GetString(4);
                    items.ProductId = reader.GetString(5);
                    items.OrderDate = reader.GetDateTime(6);
                    items.OrderStatus = reader.GetString(7);
                    items.Quantity = reader.GetInt32(10);

                    item.Add(items);
                }

                foreach (CartItems items in item)
                {
                    totalPrice += decimal.Parse(items.Price);
                }
            }
            return totalPrice;
        }

        //GET SINGLE PRODUCT FROM DB
        public DProducts GetSingleProduct(int productId)
        {
			OpenCloseDatabase();
			string sql = $"select * from Products where productId = {productId}";
			cmd = new SqlCommand(sql, conn);

			DProducts product = new DProducts();

			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					product.Id = reader.GetInt32(0);
					product.ProductName = reader.GetString(1);
					product.ProductDescription = reader.GetString(2);
					product.ProductCategory = reader.GetString(4);
					product.ProductPrice = reader.GetDecimal(5);
					product.Stock = reader.GetInt32(6);
					product.Img = reader.GetString(7);
				}
			}

			return product;
		}

        //EDIT PRODUCT TO THE DB
        public void UpdateProduct(int productId, string productName, string productDescription, string productCategory, decimal productPrice, int stock, string img)
        {
            OpenCloseDatabase();
		    string sql = $"UPDATE Products SET name='{productName}', description='{productDescription}', category='{productCategory}', price='{productPrice}', stock='{stock}', images='{img}' WHERE productId = '{productId}'";
			SqlCommand cmd = new SqlCommand(sql, conn);
			// Execute your query
			cmd.ExecuteNonQuery();
		}

        //GET ARTISAN PRODUCTS
        public List<DProducts> GetArtisanProducts(string artisanId)
        {
            OpenCloseDatabase();
            string sql = $"select * from Products where artisanId={artisanId}";
            cmd = new SqlCommand(sql, conn);

            List<DProducts> products = new List<DProducts>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DProducts product = new DProducts();

                    product.Id = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.ProductDescription = reader.GetString(2);
                    product.ProductCategory = reader.GetString(4);
                    product.ProductPrice = reader.GetDecimal(5);
                    product.Stock = reader.GetInt32(6);
                    product.Img = reader.GetString(7);

                    products.Add(product);
                }
            }

            return products;
        }

        //DELETE PRODUCTS FROM DB
        public void DeleteProduct(string userId, string productId)
        {
            OpenCloseDatabase();

            string sql = $"DELETE FROM Products WHERE artisanId ='{userId}' AND productId ='{productId}'";
			cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
		}

        //CART FEATURE
        public void CartFeauture(int userId, int productId)
        {
            OpenCloseDatabase();

            string sql = $"IF EXISTS (SELECT 1 FROM cart WHERE productId = '{productId}') BEGIN    UPDATE cart SET quantity = quantity + '1' WHERE productId = '{productId}'; END   ELSE    BEGIN    INSERT INTO cart (productId, userId, quantity) VALUES ('{productId}', '{userId}', 1); END";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        //GET CART ITEMS
        public List<DProducts> GetItems(int userId)
        {
            OpenCloseDatabase();

            string sql = $"SELECT p.productId AS product_id, p.name AS name, p.price AS product_price, p.images AS product_img, c.quantity AS product_quantity FROM Products p JOIN Cart c ON p.productId = c.productId WHERE c.userId = '{userId}';";
            cmd = new SqlCommand(sql, conn);

            List<DProducts> cartItems = new List<DProducts>();

            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DProducts cartItem = new DProducts();

                    cartItem.Id = reader.GetInt32(0);
                    cartItem.ProductName = reader.GetString(1);
                    cartItem.Img = reader.GetString(3);
                    cartItem.Quantity = reader.GetInt32(4);
                    cartItem.ProductPrice = reader.GetDecimal(2);

                    cartItems.Add(cartItem);
                }
            }

            return cartItems;
        }

    }
}