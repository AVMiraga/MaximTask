using FluentValidation;

namespace MaximTask.Business.ViewModel
{
    public class UpdateServiceVm
    {
        public int Id { get; set; }
        public string IconCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateServiceVmValidator : AbstractValidator<UpdateServiceVm>
    {
        public UpdateServiceVmValidator()
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
