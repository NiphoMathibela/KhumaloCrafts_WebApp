using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient("DefaultEndpointsProtocol=https;AccountName=st10460431blobstorage;AccountKey=3JOrS/ireK4Wj3KC2TRMZJiy2pXnbjMKZ8jNA3Vn43qS5Vx/ogmwjImZD3XmmipOzJGe38juSaf9+AStnKm15A==;EndpointSuffix=core.windows.net");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();

app.MapRazorPages();

app.MapControllers();

app.Run();
