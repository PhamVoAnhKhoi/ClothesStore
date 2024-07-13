using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection; 
using ClothesStore.Models; 

var builder = WebApplication.CreateBuilder(args); 

// Đăng ký dịch vụ MVC với các view
builder.Services.AddControllersWithViews(); 

// Cấu hình kết nối cơ sở dữ liệu
builder.Services.AddDbContext<StoreDBContext>(opts => 
{ 
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ClothesStoreConnection"]); 
}); 

// Đăng ký kho lưu trữ sản phẩm
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>(); 

// Đăng ký service cho hóa đơn
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

// Đăng ký kho lưu trữ người dùng
builder.Services.AddScoped<IUserRepository, EFUserRepository>();

// Đăng ký kho lưu trữ Category
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// Đăng ký Razor Pages 
builder.Services.AddRazorPages(); 

// Đăng ký dịch vụ bộ nhớ đệm phân tán
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(); 

// Đăng ký dịch vụ Cart với session
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); 

// Đăng ký IHttpContextAccessor để truy cập HttpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

// Đăng ký Blazor Server
builder.Services.AddServerSideBlazor();

var app = builder.Build(); 

// Cấu hình middleware để phục vụ các file tĩnh
app.UseStaticFiles(); 

// Sử dụng session
app.UseSession(); 

// Cấu hình tuyến đường mặc định cho MVC
app.MapDefaultControllerRoute();

// Đăng ký Razor Pages 
app.MapRazorPages(); 

// Đăng ký Blazor Hub
app.MapBlazorHub(); 

// Đăng ký fallback route cho trang admin
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

// Định tuyến cho phân trang
app.MapControllerRoute("pagination", "/Page{productPage}", 
    new { Controller = "Product", action = "Index" });

app.Run();
