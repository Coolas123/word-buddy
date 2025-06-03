import { Component } from "@angular/core"
import { FormsModule, ReactiveFormsModule } from "@angular/forms"
import { LoginUser } from "../../models/loginUser.models/loginUser.model"
import { loginUserFormGroup } from "../../models/loginUser.models/loginUserFrom.model"
import { Model } from "../../models/loginUser.models/repository.loginUser.model"
import { HttpErrorResponse } from "@angular/common/http"
import { NavigationCancel, NavigationEnd, Router } from "@angular/router"
import { filter } from 'rxjs/operators';
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Message } from "../../models/alertMessage.models/message.model"
import { JwtHelperService } from "@auth0/angular-jwt"

@Component({
    selector: "loginUserForm",
    templateUrl: "./LoginUserForm.component.html",
    imports: [FormsModule, ReactiveFormsModule],
    providers: [JwtHelperService]
})

export class LoginUserFormComponent {
    loginUser: LoginUser = new LoginUser()
    formSubmitted: boolean = false
    formGroup: loginUserFormGroup = new loginUserFormGroup()

    constructor(private model: Model,private router: Router, private messageService: MessageService) {
    }
    
    submitForm() {
        Object.assign(this.loginUser, this.formGroup.value)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.authenticate(this.loginUser).subscribe(
                {
                    next: (v) => {
                    this.router.navigateByUrl("/")
                    this.router.events.pipe(
                        filter((event: any) => event instanceof NavigationEnd|| event instanceof NavigationCancel)
                    ).subscribe(_ => this.messageService.reportMessage(new Message(v.message)));
                },
                error: (e: HttpErrorResponse) => {
                    this.setCustomErrors(e.error?.errors)
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
}