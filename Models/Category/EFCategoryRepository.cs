// EFCategoryRepository.cs
using System.Linq;

namespace ClothesStore.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private StoreDBContext context;

        public EFCategoryRepository(StoreDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Category> Categories => context.Categories;

        public void CreateCategory(Category category)
        {
            context.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Remove(category);
            context.SaveChanges();
        }

        public void SaveCategory(Category category)
        {
            context.SaveChanges();
        }
    }
}
