using aspnetcore.ntier.DTO.DTOs;
using FluentValidation;

namespace aspnetcore.ntier.BLL.Validators;

public class UserToLoginDTOValidator : AbstractValidator<UserToLoginDTO>
{
    public UserToLoginDTOValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
