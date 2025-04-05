import { Component } from "@angular/core"
import { Model } from "../../models/registerUser.models/repository.registerUser.model"
import { RegisterUser } from "../../models/registerUser.models/registerUser.model"
import { FormsModule, ReactiveFormsModule } from "@angular/forms"
import { registerUserFormGroup } from "../../models/registerUser.models/registerUserFrom.model"
import { Country } from "../../enums/country";
import { HttpErrorResponse } from "@angular/common/http"
import { NavigationEnd, Router } from "@angular/router";
import { filter } from 'rxjs/operators';
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Message } from "../../models/alertMessage.models/message.model"
import { LoginUser } from "../../models/loginUser.models/loginUser.model"


@Component({
    selector: "registerUserForm",
    templateUrl: "./registerUserForm.component.html",
    imports: [FormsModule, ReactiveFormsModule],
    providers: [Model]
})

export class RegisterUserFormComponent {
    newUser: RegisterUser = new RegisterUser()
    formSubmitted: boolean = false
    formGroup: registerUserFormGroup = new registerUserFormGroup()
    countries = Country

    constructor(private model: Model, private router: Router, private messageService: MessageService) {
    }

    submitForm() {
        Object.assign(this.newUser, this.formGroup.value)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.saveUser(this.newUser).subscribe({
                next: (res) => {  
                    this.model.authenticate(res.jwt);
                    this.router.navigateByUrl("/")
                    this.router.events.pipe(
                        filter((event: any) => event instanceof NavigationEnd)
                    ).subscribe(_ => this.messageService.reportMessage(new Message(res.message)));
                },
                error: (e: HttpErrorResponse) => {
                    this.setCustomErrors(e.error)
                    this.formSubmitted = false
                },
                complete: () => {
                    this.formSubmitted = false
                }
            })
        }
        else this.formSubmitted = false
    }
    private setCustomErrors(errors: any) {
        for (let i = 0; i < errors.length; i++) {
            var control = this.formGroup.registerUserControls.find(x => x.modelProperty === errors[i].Code)
            if (control) {
                var serverError = errors[i].Code
                control.setErrors({ serverError: errors[i].Message })
                control.markAsDirty()
                control.markAsTouched()
            }
        }
    }

    getCountries(): any {
        return Object
            .keys(this.countries)
            .filter((v) => isNaN(Number(v)) && v != "None")
    }
}