using Microsoft.EntityFrameworkCore; 
namespace ClothesStore.Models 
{ 
    public static class SeedData 
    { 
        public static void EnsurePopulated(IApplicationBuilder app) 
        { 
            StoreDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDBContext>(); 
            if (context.Database.GetPendingMigrations().Any()) 
            { 
                context.Database.Migrate(); 
            } 
            if (!context.Products.Any()) 
            { 
                context.Products.AddRange( 
                    new Product 
                    { 
                        Name = "Bộ QA bóng rổ ", 
                        Description = "Bộ QA bóng rổ Bé trai YATU027-1V", 
                        
                        Price = 275,
                        ProductImage = "https://vn-test-11.slatic.net/p/8cb9a1e49439a7b93b53d1454f388862.jpg"
                    }
                );
                context.SaveChanges(); 
            } 
            // if (!context.Users.Any()) 
            // { 
            //     context.Users.AddRange( 
            //         new User 
            //         { 
            //             Name = "",
            //             UserName = "",
            //             Password = "",
            //             //ConfirmPassword = "",
            //             Phone = ""
            //         }
            //     );
            //     context.SaveChanges(); 
            // }
        } 
    } 
}
                    