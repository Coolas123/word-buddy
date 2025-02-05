using Application.HelpClasses;
using Application.Users.Commands.ChangeSettings;
using Domain.Enums;
using Domain.Shared;
using Presentation.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace User.Api.IntegrationTests
{
    public class SettingControllerTests
    {
        [Fact]
        public async Task Get_ReturnsStatusCode200_WhenUserExist() {
            //Arrange
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                "dima@mail.ru",
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            var expectedResult = SettingUserViewModel.Create(userResult.Value());

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();

                await application.SetDataAsync(userResult.Value());

                var jwtGenerator = application.GetDependency<JWTGenerator>();
                var jwtToken = jwtGenerator.Create(userResult.Value());
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");

                //Act
                var responseGet = await client.GetAsync("https://localhost:5011/Settings");
                //Assert
                Assert.NotNull(responseGet);

                var responseMessage = await responseGet.Content.ReadFromJsonAsync<SettingUserViewModel>();
                Assert.Equivalent(expectedResult,responseMessage,strict:true);

                Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.OK));
            }
        }

        [Fact]
        public async Task Get_ReturnsStatusCode204_WhenUserNotExisting() {
            //Arrange
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                "dima@mail.ru",
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();

                var jwtGenerator = application.GetDependency<JWTGenerator>();
                var jwtToken = jwtGenerator.Create(userResult.Value());
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");

                //Act
                var responseGet = await client.GetAsync("https://localhost:5011/Settings");
                //Assert
                Assert.NotNull(responseGet);

                var responseMessage = await responseGet.Content.ReadAsStringAsync();
                Assert.True(!string.IsNullOrWhiteSpace(responseMessage));

                Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.NoContent));
            }
        }

        [Fact]
        public async Task Put_ReturnsStatusCode200_WhenUserSettingsSuccessfulyUpdated() {
            //Arrange
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                "dima@mail.ru",
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            var changeUserSettings = new ChangeUserSettingsCommand
            {
                UserName = "dima_CHANGED"
            };

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();

                await application.SetDataAsync(userResult.Value());

                var jwtGenerator = application.GetDependency<JWTGenerator>();
                var jwtToken = jwtGenerator.Create(userResult.Value());
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");

                //Act
                var responsePut = await client.PutAsJsonAsync("https://localhost:5011/Settings", changeUserSettings);
                //Assert
                Assert.NotNull(responsePut);

                var responseMessage = await responsePut.Content.ReadAsStringAsync();
                Assert.True(!string.IsNullOrWhiteSpace(responseMessage));

                Assert.True(responsePut.StatusCode.Equals(HttpStatusCode.OK));
            }
        }

        [Fact]
        public async Task Put_ReturnsStatusCode400_WhenUserSettingsWasNotSuccessfulyUpdated() {
            //Arrange
            var userResult = await Domain.Entities.User.CreateAsync(
                Guid.NewGuid(),
                "dima",
                "dima@mail.ru",
                Country.Russia,
                HashPassword.Generate("aA1@123456"),
                SystemRole.User,
                UserRole.WordLearner
            );

            var changeUserSettings = new ChangeUserSettingsCommand
            {
                UserName = "dima_CHANGED"
            };

            using (var application = new UserWebApplicationFactory()) {
                var client = application.CreateClient();

                var jwtGenerator = application.GetDependency<JWTGenerator>();
                var jwtToken = jwtGenerator.Create(userResult.Value());
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwtToken}");

                //Act
                var responsePut = await client.PutAsJsonAsync("https://localhost:5011/Settings", changeUserSettings);
                //Assert
                Assert.NotNull(responsePut);

                var responseErrors = await responsePut.Content.ReadFromJsonAsync<Result>();
                Assert.NotNull(responseErrors);

                Assert.True(responsePut.StatusCode.Equals(HttpStatusCode.BadRequest));
            }
        }
    }
}
