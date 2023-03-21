using FluentValidation;

namespace RoomConfigMicroservice.Commands.Hotel;

public class CreateHotelValidator : AbstractValidator<CreateHotelCommand>
{
	public CreateHotelValidator()
	{
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty");

        RuleFor(v => v.Description)
            .NotEmpty()
            .WithMessage("Description can't be empty");
    }
}