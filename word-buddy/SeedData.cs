using Application.Users.Commands.RegisterUser;
using Domain.Enums;
using Domain.Repositories;
using MediatR;
using Persistence;

namespace word_buddy
{
    public static class SeedData
    {
        public static async Task Fill(IApplicationBuilder app) {
            var dbContext = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var sender = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ISender>();

            if (!dbContext.Users.Any()) {
                var registerUserCommand = new RegisterUserCommand
                {
                    UserName="anton",
                    Country= Country.Russia,
                    Password="aA1@123456",
                    ConfirmPassword= "aA1@123456",
                    Email="anton@mail.ru"
                };

                var r = await sender.Send(registerUserCommand);
            }
        }
    }
}
