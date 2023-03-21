using FluentValidation;

namespace RoomConfigMicroservice.Commands.Room;

public class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
{
	public UpdateRoomValidator()
	{
        RuleFor(v => v.Id)
            .NotEmpty()
            .WithMessage("Id can't be empty");

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