using Domain.Entities;
using Domain.Enums;

namespace Presentation.ViewModels
{
    public sealed class SettingUserViewModel
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public Country Country { get; init; }

        public static SettingUserViewModel Create(User patron) {
            return new SettingUserViewModel
            {
                UserName = patron.UserName,
                Email = patron.Email,
                Country = patron.Country,
            };
        }
    }
}
