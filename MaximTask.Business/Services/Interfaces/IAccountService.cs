using MaximTask.Business.ViewModel.Account;
using MaximTask.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace MaximTask.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResult> CheckCredentials(LoginVm vm);
        Task<RegisterResult> Register(RegisterVm vm);
        Task CreateRole();
    }

    public class RegisterResult
    {
        public IdentityResult IdentityResult { get; set; }
        public AppUser? AppUser { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public AppUser? User { get; set; }
    }
}
