using FluentValidation.Results;
using MaximTask.Business.Services.Interfaces;
using MaximTask.Business.ViewModel.Account;
using MaximTask.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaximTask.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<AppUser> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? returnUrl)
        {
            LoginVmValidator validator = new LoginVmValidator();
            ValidationResult result = validator.Validate(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            var res = await _accountService.CheckCredentials(vm);

            if (!res.Success || res.User ==  null)
            {
                ModelState.AddModelError("", "Username Or Password is wrong!");

                return View(vm);
            }

            await _signInManager.SignInAsync(res.User, true);

            if (returnUrl != null && !(returnUrl.Contains("Register") || returnUrl.Contains("Login")))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm, string? returnUrl)
        {
            RegisterVmValidator validator = new RegisterVmValidator();
            ValidationResult result = validator.Validate(vm);

            if (!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            var res = await _accountService.Register(vm);

            if(!res.IdentityResult.Succeeded || res.AppUser == null)
            {
                ModelState.AddModelError("", "Something Went Wrong!");
                return View(vm);
            }

            await _signInManager.SignInAsync(res.AppUser, false);

            if (returnUrl != null && !(returnUrl.Contains("Register") || returnUrl.Contains("Login")))
                return Redirect(returnUrl);
        
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
