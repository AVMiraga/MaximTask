using FluentValidation;

namespace MaximTask.Business.ViewModel
{
    public class CreateServiceVm
    {
        public string IconCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreateServiceVmValidator : AbstractValidator<CreateServiceVm>
    {
        public CreateServiceVmValidator() 
        {
            RuleFor(x => x.IconCode)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
