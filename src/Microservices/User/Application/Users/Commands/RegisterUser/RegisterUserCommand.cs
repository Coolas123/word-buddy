using Application.Abstractions.Messaging;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : ICommand<string>
    {
        [Display(Name = "Прозвище")]
        public string? UserName { get; set; }

        [Display(Name = "Страна")]
        public Country Country { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Почта")]
        public string? Email { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
