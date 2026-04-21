using Application.Dtos.Requests;
using FluentValidation;

namespace Api.Validators {
    public class RequestObjValidator : AbstractValidator<RequestObj> {
        public RequestObjValidator()
        {
            RuleFor(x => x.RequestArrayObj)
                .NotEmpty()
                .NotNull()
                .Must(x => x!.Distinct().Count() >= 2)
                .WithMessage("RequestArrayObj must contain at least two distinct integers.");
        }
    }
}
