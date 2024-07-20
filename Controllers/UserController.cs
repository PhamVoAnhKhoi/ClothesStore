using ClothesStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClothesStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountRepository accountRepo;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(IAccountRepository repo, SignInManager<ApplicationUser> signInManager)
        {
            accountRepo = repo;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpModel);
            }

            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(signUpModel);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signInModel);
            }

            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(signInModel);
            }

            // Add JWT token to cookies or other storage for later use
            Response.Cookies.Append("jwtToken", result, new Microsoft.AspNetCore.Http.CookieOptions { HttpOnly = true });

            if(signInModel.Email == "anhkhoiphamvo2909@gmail.com"){
                return RedirectToAction("Index", "Admin");
            }
            else{
                 return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            Response.Cookies.Delete("jwtToken"); // Xóa token hoặc lưu trữ xác thực khác
            return RedirectToAction("Index", "Home");
        }
    }
}
