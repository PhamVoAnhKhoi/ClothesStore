using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection; 
using ClothesStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args); 

// Đăng ký dịch vụ MVC với các view
builder.Services.AddControllersWithViews(); 
//Đăng ký service cho việc xử lý Identity

builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<StoreDBContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new
        Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT: ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (builder.Configuration["JWT:Secret"]))
    };
});

// Cấu hình kết nối cơ sở dữ liệu
builder.Services.AddDbContext<StoreDBContext>(opts => 
{ 
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ClothesStoreConnection"]); 
}); 

// Đăng ký kho lưu trữ sản phẩm
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>(); 

// Đăng ký service cho hóa đơn
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

// Đăng ký kho lưu trữ Category
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<IAccountRepository, EFAccountRepository>();

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
