using MaximTask.Business.Enums;
using MaximTask.Business.Services.Interfaces;
using MaximTask.Business.ViewModel.Account;
using MaximTask.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace MaximTask.Business.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResult> CheckCredentials(LoginVm vm)
        {
            AppUser user = await _userManager.FindByEmailAsync(vm.LoginId) ?? await _userManager.FindByNameAsync(vm.LoginId);

            if (user == null)
            {
                return new LoginResult()
                {
                    User = null,
                    Success = false
                };                
            }

            bool res = await _userManager.CheckPasswordAsync(user, vm.Password);

            return new LoginResult()
            {
                User = user,
                Success = res
            };
        }

        public async Task CreateRole() // Check Later
        {
            foreach (var item in Enum.GetValues(typeof(MyRoles)))
            {
                IdentityRole role = new()
                {
                    Name = item.ToString()
                };

                await _roleManager.CreateAsync(role);
            }
        }

        public async Task<RegisterResult> Register(RegisterVm vm)
        {
            AppUser user = new()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                UserName = vm.UserName,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            string RoleToAdd = MyRoles.Moderator.ToString(); //Change if needed => Roles : Admin, Moderator, User

            if (result.Succeeded) 
            {
                if(await _roleManager.RoleExistsAsync(RoleToAdd))
                    await _userManager.AddToRoleAsync(user, RoleToAdd);
                else
                {
                    IdentityRole role = new()
                    {
                        Name = RoleToAdd
                    };

                    await _roleManager.CreateAsync(role);

                    await _userManager.AddToRoleAsync(user, RoleToAdd);
                }
            }

            return new RegisterResult()
            {
                AppUser = user,
                IdentityResult = result
            };
        }
    }
}
