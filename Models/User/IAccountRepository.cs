using Microsoft.AspNetCore.Identity;
namespace ClothesStore.Models
{
    public interface IAccountRepository
    {
        public Task<IdentityResult>SignUpAsync(SignUpModel model);
        public Task<string>SignInAsync(SignInModel model);
    }
}
