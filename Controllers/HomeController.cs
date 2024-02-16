using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Task_four.Models;

namespace Task_four.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }

            return View();
        }

        [HttpPost("ManageUsers")]
        public async Task<IActionResult> ManageUsers(List<string> selectedUsers, string action)
        {
            if (selectedUsers.Count > 0)
            {
                foreach (var userId in selectedUsers)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (action == "Lock")
                    {
                        user.LockoutEnd = DateTime.MaxValue;
                        user.UserStatus = Enums.UserStatus.Blocked;
                        if (user == await _userManager.GetUserAsync(User))
                        {
                            await _signInManager.SignOutAsync();
                        }
                        
                        await _userManager.UpdateAsync(user);
                    }
                    else if (action == "Unlock")
                    {
                        user.LockoutEnd = null;
                        user.UserStatus = Enums.UserStatus.Active;
                        await _userManager.UpdateAsync(user);
                    }
                    else if (action == "Delete")
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
