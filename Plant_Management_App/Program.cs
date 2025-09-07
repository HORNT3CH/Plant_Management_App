using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plant_Management_App;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SupplyPurchasesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupplyPurchasesContext") ?? throw new InvalidOperationException("Connection string 'SupplyPurchasesContext' not found.")));
builder.Services.AddDbContext<SuppliersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SuppliersContext") ?? throw new InvalidOperationException("Connection string 'SuppliersContext' not found.")));
builder.Services.AddDbContext<SuppliesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SuppliesContext") ?? throw new InvalidOperationException("Connection string 'SuppliesContext' not found.")));
builder.Services.AddDbContext<EnvironmentLogsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EnvironmentLogsContext") ?? throw new InvalidOperationException("Connection string 'EnvironmentLogsContext' not found.")));
builder.Services.AddDbContext<OrderDetailsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDetailsContext") ?? throw new InvalidOperationException("Connection string 'OrderDetailsContext' not found.")));
builder.Services.AddDbContext<OrdersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersContext") ?? throw new InvalidOperationException("Connection string 'OrdersContext' not found.")));
builder.Services.AddDbContext<InventoriesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoriesContext") ?? throw new InvalidOperationException("Connection string 'InventoriesContext' not found.")));
builder.Services.AddDbContext<CustomersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomersContext") ?? throw new InvalidOperationException("Connection string 'CustomersContext' not found.")));
builder.Services.AddDbContext<GreenhousesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GreenhousesContext") ?? throw new InvalidOperationException("Connection string 'GreenhousesContext' not found.")));
builder.Services.AddDbContext<PlantBatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlantBatchContext") ?? throw new InvalidOperationException("Connection string 'PlantBatchContext' not found.")));
builder.Services.AddDbContext<PlantContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlantContext") ?? throw new InvalidOperationException("Connection string 'PlantContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
