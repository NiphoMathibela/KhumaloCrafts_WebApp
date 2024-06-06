using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Resource;
using Grpc.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace FunctionApp1
{
    public static class OrderFunction
    {
        [FunctionName("OrderFunction")]
        public static async Task Run(OrderDetails orderDetails, IDurableOrchestrationContext context, ILogger log)
        {
            await context.CallActivityAsync(nameof(Order), orderDetails);

            log.LogInformation($"Processing order from user with ID {orderDetails.CustomerId}");

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
        }

        [FunctionName(nameof(SayHello))]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation("Saying hello to {name}.", name);
            return $"Hello {name}!";
        }

        //Order function
        [FunctionName(nameof(Order))]
        public static void Order([ActivityTrigger] string customerId, ILogger log) 
        {

        string connectionString = @"Server=tcp:st10460431.database.windows.net,1433;Initial Catalog=KhumaloDB;Persist Security Info=False;User ID=nipho;Password=Excellent28@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


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

            string sql = $"INSERT INTO Orders (CustomerId, OrderDate, Total, Status) VALUES ({customerId}, GETDATE(), '2000', 'Unprocessed');";

            OpenCloseDatabase();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
    }

        [FunctionName("OrderFunction_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            //string instanceId = await starter.StartNewAsync("OrderFunction", null);

            //log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            //New logic
            var request = new HttpRequestMessage();
            var query = request.RequestUri.ParseQueryString();
            // ... process the HTTP request body to get the input data
            string customerId = query["customerId"];  //Accessing query parameter data
            string status = "Unprocessed";
            DateTime date = DateTime.Now;
            Decimal total = 0;

            // ... prepare the input object for the orchestrator function
            OrderDetails orderDetails = new OrderDetails { CustomerId = customerId, Date = date, Total = total, Status = status };

            // ... start the orchestration
            string instanceId = await starter.StartNewAsync("OrderFunction", orderDetails);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }


    }

    //Custom Classes
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }  // Add additional input properties as needed

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }
        //public List<Product> Products { get; set; }  // Example for complex data structures
    }
}