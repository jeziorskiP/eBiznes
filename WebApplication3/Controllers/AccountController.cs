using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Auto;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
         
        public AccountController(UserManager<IdentityUser> userManager, 
                                    SignInManager<IdentityUser> signInManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");  
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,false,false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Role model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
                if (identityResult.Succeeded)
                    return RedirectToAction("GetRoles", "Account");
            }

            return View();
        }

        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);

            if(role == null)
            {
                Console.WriteLine("CHUUUUJ!");
                return View("Error");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("GetRoles", "Account");
                }
                return RedirectToAction("GetRoles", "Account");
            }
        }


        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                Console.WriteLine("CHUUUUJ!");
                return View("Error");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("GetUsers", "Account");
                }
                return RedirectToAction("GetUsers", "Account");
            }
        }

        public IActionResult GetUsers()
        {
            var roles = _userManager.Users;
            return View(roles);
        }



        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> EditUsersInRole(string RoleId)
        {
            ViewBag.RoleId = RoleId;

            var role = await _roleManager.FindByIdAsync(RoleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Rola o Id: {RoleId} nie istnieje";
                return View("NotFound");
            }

            var model = new List<UserRole>();

            foreach (var item in _userManager.Users)
            {
                var userRole = new UserRole
                {
                    UserId = item.Id,
                    UserName = item.UserName
                };

                if (await _userManager.IsInRoleAsync(item, role.Name))
                    userRole.IsSelected = true;
                else
                    userRole.IsSelected = false;
                model.Add(userRole);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRole> model, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
                return View("Not found");
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    continue;

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("GetRoles", "Account");
                }



            }
            return RedirectToAction("GetRoles", "Account");
        }





    }
}
