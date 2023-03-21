using FluentValidation;

namespace RoomConfigMicroservice.Commands.Furniture;

public class CreateFurnitureValidator : AbstractValidator<CreateFurnitureCommand>
{
	public CreateFurnitureValidator()
	{
		RuleFor(v => v.Name)
			.NotEmpty()
			.WithMessage("Name can't be empty");

		RuleFor(v => v.Description)
			.NotEmpty()
			.WithMessage("Description can't be empty");
	}
}