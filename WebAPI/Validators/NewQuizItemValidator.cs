using FluentValidation;

public class NewQuizItemValidator : AbstractValidator<NewQuizItemDTO>
{
    public NewQuizItemValidator()
    {
        RuleFor(q => q.Question)
            .MaximumLength(200).WithMessage("Pytanie nie może być dłuższe niż 200 znaków.")
            .MinimumLength(3).WithMessage("Pytanie nie może być krótsze od 3 znaków!");
        RuleForEach(q => q.Options)
            .MaximumLength(200)
            .MinimumLength(1);
        RuleFor(q => q.CorrectOptionIndex)
            .Must( (item, index) => index > -1 && index < item.Options.Count);
    }
}