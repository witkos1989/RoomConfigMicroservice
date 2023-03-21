using FluentValidation;

namespace RoomConfigMicroservice.Commands.RoomType;

public class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeCommand>
{
    public UpdateRoomTypeValidator()
    {
        RuleFor(rt => rt.Id)
            .NotEmpty()
            .WithMessage("Id can't be empty");

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