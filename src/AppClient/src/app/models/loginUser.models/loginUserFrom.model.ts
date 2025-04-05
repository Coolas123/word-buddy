import { FormControl, FormGroup, Validators } from "@angular/forms"

export class loginUserFormControl extends FormControl {
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

export class loginUserFormGroup extends FormGroup {
    constructor() {
        super({
            Email: new loginUserFormControl("Почта", "Email", "",
                Validators.compose([Validators.required, Validators.email])),

            Password: new loginUserFormControl("Пароль", "Password", "", Validators.required),
        })
    }

    get registerUserControls(): loginUserFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as loginUserFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.registerUserControls.forEach(c => c.getValidationMessages()
            .forEach(m => messages.push(m)));
        return messages;
    }
}