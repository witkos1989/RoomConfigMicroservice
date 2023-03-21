using FluentValidation;

namespace RoomConfigMicroservice.Commands.Room;

public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
{
	public CreateRoomValidator()
	{
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty");

        RuleFor(v => v.Description)
            .NotEmpty()
            .WithMessage("Description can't be empty");

        RuleFor(v => v.CurrentPrice)
            .NotEmpty()
            .WithMessage("Price can't be empty")
            .GreaterThan(0)
            .WithMessage("Price can't be lower than 0");
    }
}