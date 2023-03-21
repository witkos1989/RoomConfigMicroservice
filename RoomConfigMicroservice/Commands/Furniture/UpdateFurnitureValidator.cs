using FluentValidation;

namespace RoomConfigMicroservice.Commands.Furniture;

public class UpdateFurnitureValidator : AbstractValidator<UpdateFurnitureCommand>
{
    public UpdateFurnitureValidator()
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
    }
}