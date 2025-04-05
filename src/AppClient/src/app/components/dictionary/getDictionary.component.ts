import { Component, inject } from "@angular/core"
import { AbstractControl, FormArray, FormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms"
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { RouterModule } from '@angular/router';
import { Model } from "../../models/getDictionary.models/repository.getDictionary.model"
import { debounceTime, distinctUntilChanged, filter, Subject } from "rxjs"
import { Message } from "../../models/alertMessage.models/message.model"
import { GetDictionaryFormControl, GetDictionaryFormGroup } from "../../models/getDictionary.models/getDictionaryForm.model"
import { HttpErrorResponse } from "@angular/common/http"
import { UpdateDictionaryAndRowsCommand } from "../../models/getDictionary.models/getDictionary.model";
import { Language } from "../../enums/language";
import { AiService } from "../../models/ai.models/generateWordContext.repository";
import { GenerateTextContext } from "../../models/ai.models/generateWordContext.model";

@Component({
    selector: "dictionaries",
    templateUrl: "./getDictionary.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule],
    providers: []
})

export class GetDictionaryComponent {
    newDictionary: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    oldDictionary: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    formGroup: GetDictionaryFormGroup = new GetDictionaryFormGroup(inject(FormBuilder), this.newDictionary)
    formSubmitted: boolean = false
    formChanged: boolean = false
    languages = Language
    inputSubject = new Subject()
    openMenuFlag: boolean = false
    menuCloseSubject = new Subject()
    wordContext: string =""

    constructor(private aiService:AiService, private model: Model, private router: Router, private messageService: MessageService, private activeRoute: ActivatedRoute) {
        this.inputSubject.pipe(
            debounceTime(100),
            distinctUntilChanged())
            .subscribe(value => {
                this.addNewRow();
            });

        this.aiService.getGeneratedWordContextObservable().subscribe(x=>{
            // this.wordContext+=x;
            // this.newDictionary.UpdateWordsCommand?.Words
        })

        this.model.getDictionaryObservable().subscribe(dictionary => {
            this.newDictionary = dictionary
            this.oldDictionary = dictionary
            console.log(dictionary)
            this.formGroup = new GetDictionaryFormGroup(inject(FormBuilder), this.newDictionary)
        })

        this.formGroup.valueChanges.subscribe(value => {
            this.formChanged = this.isChangedDictionary() || this.isChangedWords()
        })
    }

    submitForm() {
        Object.assign(this.newDictionary, this.formGroup.value)
        console.log(this.newDictionary)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.updateDictionary(this.newDictionary).subscribe({
                next: (v) => {
                    this.messageService.reportMessage(new Message(v))
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
            var control = this.formGroup.getDictionaryControls.find(x => x.modelProperty === errors[i].Code)
            if (control) {
                var serverError = errors[i].Code
                control.setErrors({ serverError: errors[i].Message })
                control.markAsDirty()
                control.markAsTouched()
            }
        }
    }

    getPompt(event:any){
        this.openMenuFlag= true;
        var idstr =  event.currentTarget.attributes.id.value;
        var id = idstr[idstr.length-1]
        //this.menu.generateContextWord.Prompt = this.newDictionary.UpdateWordsCommand?.Words[id].Text 
    }

    generateWordContext(wordId: number){
        //this.aiService.generateWordContext(this.menu.generateContextWord)
    }

    getControl(row: AbstractControl): GetDictionaryFormControl {
        return row as GetDictionaryFormControl
    }

    getLanguage(): any {
        return Object
            .keys(this.languages)
            .filter((v) => isNaN(Number(v)) && v != "None")
    }

    getRows(): FormArray<GetDictionaryFormGroup> {
        return this.formGroup.getRows() as FormArray<GetDictionaryFormGroup>
    }

    isRowsEmpty() {
        return Number(this.getRows().controls[0].controls["length"]) > 1
    }

    getNewRows(): FormArray<GetDictionaryFormGroup> {
        return this.formGroup.getNewRows() as FormArray<GetDictionaryFormGroup>
    }

    addNewRow() {
        var lastRow = this.formGroup.controls["NewDictionaryRows"].getRawValue().at(-1)
        if (lastRow.NewWord != "" && lastRow.NewTranslation != "") {
            this.formGroup.addNewRow()
        }
    }

    isChangedWords() {
        return this.formGroup.value.NewRows.length > 1 ||
            (Object.keys(this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows!).some(key =>
                this.formGroup.value.Rows[Number(key)].Word != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows![Number(key)].WordText ||
                this.formGroup.value.Rows[Number(key)].LearnStatus != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows![Number(key)].LearnStatus ||
                this.formGroup.value.Rows[Number(key)].Translation != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows![Number(key)].WordTranslation
            )
                ||
                this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows.length != this.formGroup.value.Rows.length
            )
    }

    isChangedDictionary(): boolean {
        return this.oldDictionary.UpdateDictionaryCommand?.Title != this.formGroup.value.Title ||
            this.oldDictionary.UpdateDictionaryCommand?.Description != this.formGroup.value.Description ||
            this.oldDictionary.UpdateDictionaryCommand?.WordLanguage != this.formGroup.value.WordLanguage ||
            this.oldDictionary.UpdateDictionaryCommand?.TranslationLanguage != this.formGroup.value.TranslationLanguage
    }
}