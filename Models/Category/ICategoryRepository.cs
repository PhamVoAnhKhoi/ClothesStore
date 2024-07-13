// ICategoryRepository.cs
using System.Linq;

namespace ClothesStore.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void SaveCategory(Category category);
    }
}
