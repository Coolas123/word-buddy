import { Component, inject } from "@angular/core"
import { AbstractControl, FormArray, FormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms"
import { HttpErrorResponse } from "@angular/common/http"
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Message } from "../../models/alertMessage.models/message.model"
import { CreateDictionaryAndRowsCommand } from "../../models/createDictionary.models/createDictionary.model"
import { CreateDictionaryFormControl, CreateDictionaryFormGroup } from "../../models/createDictionary.models/createDictionaryForm.model"
import { debounceTime, distinctUntilChanged, filter, Subject } from "rxjs"
import { Language } from "../../enums/language"
import { LearnStatus } from "../../enums/learnStatus"
import { SelectUploadFileComponent } from "./selectUploadFile/selectUploadFile.component"
import { Model } from "../../models/dictionary.models/repository.dictionaries.model"

@Component({
    selector: "createDictionary",
    templateUrl: "./createDictionary.component.html",
    imports: [FormsModule, ReactiveFormsModule,SelectUploadFileComponent]
})

export class CreateDictionaryFormComponent{
    newDictionary: CreateDictionaryAndRowsCommand = new CreateDictionaryAndRowsCommand()
    formSubmitted: boolean = false
    formGroup: CreateDictionaryFormGroup = new CreateDictionaryFormGroup(inject(FormBuilder))
    languages = Language
    learnStatues : Array<{ index: number, value: string }> =[]
    learnStatuesEnum = LearnStatus
    inputSubject = new Subject()

    constructor(private model: Model, private router: Router, private messageService: MessageService, activeRoute: ActivatedRoute, private fromBuilder: FormBuilder) {
        this.inputSubject.pipe(
            debounceTime(100),
            distinctUntilChanged())
            .subscribe(value => {
                this.addNewRow();
            });
            
        activeRoute.params.subscribe(x=>{
            this.setLearnStatues();
        })
    }

    submitForm() {
        this.newDictionary = CreateDictionaryAndRowsCommand.create(this.formGroup.value)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.saveDictionary(this.newDictionary).subscribe({
                next: (v) => {
                    this.model.loadDIctionaries()
                    this.router.navigateByUrl("/dictionaries")
                    this.router.events.pipe(
                        filter((event: any) => event instanceof NavigationEnd)
                    ).subscribe(_ => this.messageService.reportMessage(new Message(v)))
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
            var control = this.formGroup.createDictionaryControls.find(x => x.modelProperty === errors[i].Code)
            if (control) {
                var serverError = errors[i].Code
                control.setErrors({ serverError: errors[i].Message })
                control.markAsDirty()
                control.markAsTouched()
            }
        }
    }

    addNewDictionaryRowsToMainFormGroup(newDicitonaryRows:FormArray){
        this.formGroup.addNewRows(newDicitonaryRows)
    }

    getControl(row: AbstractControl): CreateDictionaryFormControl {
        return row as CreateDictionaryFormControl
    }

    getLanguage(): any {
        return Object
            .keys(this.languages)
            .filter((v) => isNaN(Number(v)) && v != "None")
    }

    getRows(): FormArray<CreateDictionaryFormGroup> {
        return this.formGroup.getRows() as FormArray<CreateDictionaryFormGroup>
    }

    addNewRow() {
        var lastRow = this.formGroup.controls["Rows"].getRawValue().at(-1)
        if (lastRow.Word != "" && lastRow.Translation != "") {
            this.formGroup.addRow()
        }
    }

    private setLearnStatues(){
        let result = Array<{ index: number, value: string }>();
        Object
            .keys(LearnStatus)
            .forEach((v,i) =>{
                if(isNaN(Number(v)) && i !=0 && i!=4){
                    result.push( {
                        index: i,
                        value: Object.values(LearnStatus)[i]
                    })
                }
            })
        this.learnStatues = result;
    }
}