import { Component } from "@angular/core"
import { FormsModule, ReactiveFormsModule } from "@angular/forms"
import { Model } from "../../models/userSettings.models/repository.userSettings.model"
import { HttpErrorResponse } from "@angular/common/http"
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Message } from "../../models/alertMessage.models/message.model"
import { UserSettings } from "../../models/userSettings.models/userSettings.model"
import { UserSettingsFormGroup } from "../../models/userSettings.models/userSettingsFrom.model"
import { Country } from "../../enums/country"

@Component({
    selector: "userSettingsForm",
    templateUrl: "./userSettings.component.html",
    imports: [FormsModule, ReactiveFormsModule],
    providers: []
})

export class UserSettingsFormComponent {
    newSettings: UserSettings = new UserSettings()
    formSubmitted: boolean = false
    formGroup: UserSettingsFormGroup = new UserSettingsFormGroup(this.newSettings)
    countries = Country

    constructor(private model: Model, private router: Router, private messageService: MessageService, activeRoute: ActivatedRoute) {
        this.model.getSettingsObservable().subscribe(settings=>{
            Object.assign(this.newSettings, settings)
            this.formGroup.patchValue(this.newSettings)
        })
        
    }

    settingsChanged() {
        return JSON.stringify(this.newSettings) === JSON.stringify(this.formGroup.value)
    }

    submitForm() {
        Object.assign(this.newSettings, this.formGroup.value)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.saveSettings(this.newSettings).subscribe({
                next: (v) => {
                    this.messageService.reportMessage(new Message(v))
                    this.model.userSettings = this.newSettings
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
            var control = this.formGroup.userSettingsControls.find(x => x.modelProperty === errors[i].Code)
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