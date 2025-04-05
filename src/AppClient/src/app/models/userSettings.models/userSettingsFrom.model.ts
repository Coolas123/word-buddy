import { FormControl, FormGroup, Validators } from "@angular/forms"
import { UserSettings } from "./userSettings.model"

export class UserSettingsFormControl extends FormControl {
    label: string
    modelProperty: string

    constructor(label: string, property: string, value: any, validator?: any) {
        super(value, validator)
        this.label = label
        this.modelProperty = property
    }

    getValidationMessages() {
        let messages: string[] = []
        if (this.errors) {
            for (let errorName in this.errors) {
                switch (errorName) {
                    case "required":
                        messages.push("requered!!!");
                        break;
                    case "email":
                        messages.push("email is worng!!!");
                        break;
                    case "serverError":
                        messages.push(this.errors[errorName]);
                        break;
                }
            }
        }
        return messages
    }
}

export class UserSettingsFormGroup extends FormGroup {
    constructor(defaultValue: UserSettings) {
        super({
            UserName: new UserSettingsFormControl("Прозвище", "UserName", defaultValue.UserName),

            Email: new UserSettingsFormControl("Почта", "Email", defaultValue.Email,
                Validators.compose([Validators.required, Validators.email])),

            Country: new UserSettingsFormControl("Страна", "Country", defaultValue.Country),

            Password: new UserSettingsFormControl("Новый пароль", "Password", ""),

            ConfirmPassword: new UserSettingsFormControl("Повторите новый пароль", "ConfirmPassword", ""),
        })
    }

    get userSettingsControls(): UserSettingsFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as UserSettingsFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.userSettingsControls.forEach(c => c.getValidationMessages()
            .forEach(m => messages.push(m)));
        return messages;
    }
}