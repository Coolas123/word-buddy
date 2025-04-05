// import { FormControl, FormGroup, Validators } from "@angular/forms"
// import { Dictionary } from "./dictionary.model"

// export class DictionariesFormControl extends FormControl {
//     label: string
//     modelProperty: string

//     constructor(label: string, property: string, value: any, validator?: any) {
//         super(value, validator)
//         this.label = label
//         this.modelProperty = property
//     }

//     getValidationMessages() {
//         let messages: string[] = []
//         if (this.errors) {
//             for (let errorName in this.errors) {
//                 switch (errorName) {
//                     case "required":
//                         messages.push("requered!!!");
//                         break;
//                     case "email":
//                         messages.push("email is worng!!!");
//                         break;
//                     case "serverError":
//                         messages.push(this.errors[errorName]);
//                         break;
//                 }
//             }
//         }
//         return messages
//     }
// }

// export class DictionariesFormGroup extends FormGroup {
//     constructor(defaultValue: Dictionary) {
//         super({
//             UserName: new DictionariesFormControl("Прозвище", "UserName", defaultValue.UserName),

//             Email: new DictionariesFormControl("Почта", "Email", defaultValue.Email,
//                 Validators.compose([Validators.required, Validators.email])),

//             Country: new DictionariesFormControl("Страна", "Country", defaultValue.Country),

//             Password: new DictionariesFormControl("Новый пароль", "Password", ""),

//             ConfirmPassword: new DictionariesFormControl("Повторите новый пароль", "ConfirmPassword", ""),
//         })
//     }

//     get dictionariesControls(): DictionariesFormControl[] {
//         return Object.keys(this.controls)
//             .map(k => this.controls[k] as DictionariesFormControl);
//     }

//     getFormValidationMessages(): string[] {
//         let messages: string[] = [];
//         this.dictionariesControls.forEach(c => c.getValidationMessages()
//             .forEach(m => messages.push(m)));
//         return messages;
//     }
// }