import { FormControl, FormGroup, Validators } from "@angular/forms"
import { isPasswordEqualsValidator } from "./validators.model.ts/isPasswordEquals.validator"

export class registerUserFormControl extends FormControl {
    label: string
    modelProperty: string

    constructor(label: string, property: string, value: any, validator: any) {
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
                        messages.push("Заполните поле");
                        break;
                    case "email":
                        messages.push("Неверный формат почты");
                        break;
                    case "maxlength":
                        messages.push(`Длина не должна превышать ${this.errors[errorName].requiredLength} символа`);
                        break;
                    case "pattern":
                        messages.push("Прозвище должно состоять из латинских букв, цифр");
                        break;
                    case "minlength":
                        messages.push(`Длина должна быть не меньше ${this.errors[errorName].requiredLength} символов`);
                        break;
                    case "passwortMatch":
                        messages.push("Пароли не совпадают");
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

export class registerUserFormGroup extends FormGroup {
    constructor() {
        super({
            UserName: new registerUserFormControl("Прозвище", "UserName", "",  
                Validators.compose([Validators.required,Validators.maxLength(64), Validators.pattern("[A-Za-z0-9]+")])),

            Country: new registerUserFormControl("Страна", "Country", "", Validators.required),

            Password: new registerUserFormControl("Пароль", "Password", "", 
                Validators.compose([Validators.required, Validators.minLength(6)])),

            ConfirmPassword: new registerUserFormControl("Повторите пароль", "ConfirmPassword", "", 
                Validators.compose([ Validators.required,isPasswordEqualsValidator])),

            Email: new registerUserFormControl("Почта", "Email", "",
                Validators.compose([Validators.required, Validators.email])),
        },
    )}

    get registerUserControls(): registerUserFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as registerUserFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.registerUserControls.forEach(c => c.getValidationMessages()
            .forEach(m => messages.push(m)));
        return messages;
    }
}