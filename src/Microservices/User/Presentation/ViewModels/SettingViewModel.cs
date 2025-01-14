using Application.Users.Commands.ChangeSettings;

namespace Presentation.ViewModels
{
    public class SettingViewModel
    {
        public ChangeUserSettingsCommand? ChangeUserSettingsCommand { get; set; }
        public SettingUserViewModel? SettingUserViewModel { get; set; }

        public SettingViewModel(SettingUserViewModel settingUserViewModel) {
            SettingUserViewModel = settingUserViewModel;
            ChangeUserSettingsCommand = new();
        }
        public SettingViewModel() { }
    }
}
