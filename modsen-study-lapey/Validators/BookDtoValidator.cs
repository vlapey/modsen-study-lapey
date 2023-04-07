using FluentValidation;
using modsen_study_lapey.Dto;

namespace modsen_study_lapey.Validators;

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Iban).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Genre).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Author).NotEmpty();
        RuleFor(x => x.BookTaken).NotEmpty();
        RuleFor(x => x.BookWillBeReturned).NotEmpty();
    }
}