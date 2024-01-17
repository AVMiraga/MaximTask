using FluentValidation;

namespace MaximTask.Business.ViewModel.Account
{
    public class LoginVm
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }

    public class LoginVmValidator : AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.LoginId)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

        }
    }
}
