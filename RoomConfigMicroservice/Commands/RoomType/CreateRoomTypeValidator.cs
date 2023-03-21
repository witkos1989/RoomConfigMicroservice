using FluentValidation;

namespace RoomConfigMicroservice.Commands.RoomType;

public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeCommand>
{
	public CreateRoomTypeValidator()
	{
		RuleFor(rt => rt.Name)
			.NotEmpty()
			.WithMessage("Name can't be empty");

        RuleFor(rt => rt.Description)
            .NotEmpty()
            .WithMessage("Description can't be empty");

        RuleFor(rt => rt.PrivateBathroom)
            .NotNull()
            .WithMessage("PrivateBathroom can't be null");
    }
}