using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;

namespace ClothesStore.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository repo)
        {
            repository = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xác thực người dùng ở đây
                var user = repository.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

                if (user != null)
                {
                    // Đăng nhập thành công, có thể lưu session hoặc cookie ở đây
                    // Ví dụ: HttpContext.Session.SetString("UserName", user.UserName);
                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Thông báo lỗi đăng nhập không thành công
                }
            }
            return View(model);
        }     
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
         [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = repository.Users.FirstOrDefault(u => u.UserName == model.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    return View(model);
                }

                var newUser = new User
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Password = model.Password,
                    Phone = model.Phone
                };

                repository.AddUser(newUser);
                repository.Save();

                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
