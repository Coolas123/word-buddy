import { Component } from "@angular/core"
import { AbstractControl, FormArray, FormBuilder, FormControl, FormsModule, ReactiveFormsModule } from "@angular/forms"
import { ActivatedRoute, Router } from "@angular/router"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { RouterModule } from '@angular/router';
import { Model } from "../../models/getDictionary.models/repository.getDictionary.model"
import { Model as DictionariesModel } from "../../models/dictionary.models/repository.dictionaries.model"
import { debounceTime, distinctUntilChanged, filter, Subject } from "rxjs"
import { Message } from "../../models/alertMessage.models/message.model"
import { GetDictionaryFormControl, GetDictionaryFormGroup } from "../../models/getDictionary.models/getDictionaryForm.model"
import { HttpErrorResponse } from "@angular/common/http"
import { UpdateDictionaryAndRowsCommand } from "../../models/getDictionary.models/getDictionary.model";
import { Language } from "../../enums/language";
import { AiService } from "../../models/ai.models/generateWordContext.repository";
import { GenerateTextQuery } from "../../models/ai.models/GenerateTextQuery";
import { LearnStatus } from "../../enums/learnStatus";
import { Dictionary } from "../../models/dictionary.models/dictionary.model";
import { CommonModule } from '@angular/common';
import { MoveToAnotherDicitonaryRowsCommand, MoveToAnotherDictionary } from "../../models/getDictionary.models/moveToAnotherDictionary.model";
import { SelectUploadFileComponent } from "./selectUploadFile/selectUploadFile.component";


@Component({
    selector: "dictionaries",
    templateUrl: "./getDictionary.component.html",
    imports: [FormsModule, ReactiveFormsModule, RouterModule, CommonModule, SelectUploadFileComponent],
    providers: []
})

export class GetDictionaryComponent {
    newDictionary: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    oldDictionary: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    formGroup: GetDictionaryFormGroup
    formSubmitted: boolean = false
    formChanged: boolean = false
    languages = Language
    inputSubject = new Subject()
    wordContext: string = ""
    newLearnStatues: Array<{ index: number, value: string, key: string }> = []
    selectedLearnStatues: Array<{ index: number, value: string, key: string }> = []
    learnStatuesEnum = LearnStatus
    dictionaries: Dictionary[] = []
    selectMode: boolean = false
    moveToAnotherDictionary: MoveToAnotherDictionary
    moveToAnotherDictionaryFormSubmitted: boolean = false
    isShowWordTranslation: boolean = false
    rowForImg: number = 0
    currentImg: string = ""
    currentShowImg:string = ""
    isShowLearnStatusPanel:boolean = false
    editMode:boolean = false;


    constructor(private formBuilder: FormBuilder, private dictionariesModel: DictionariesModel, private aiService: AiService, private model: Model, private router: Router, private messageService: MessageService, private activeRoute: ActivatedRoute) {
        this.formGroup = new GetDictionaryFormGroup(formBuilder, this.newDictionary)

        this.inputSubject.pipe(
            debounceTime(100),
            distinctUntilChanged())
            .subscribe(value => {
                this.addNewRow()
            });
        activeRoute.params.subscribe(x => {
            this.setNewLearnStatues()
            this.setSelectedLearnStatues()
        })
        this.dictionariesModel.getDictionariesObservable().subscribe(x => {
            this.dictionaries = x
        })

        this.model.getDictionaryObservable().subscribe(dictionary => {
            this.newDictionary = dictionary
            this.oldDictionary = dictionary
            this.formGroup = new GetDictionaryFormGroup(formBuilder, this.newDictionary)
        })

        this.formGroup.valueChanges.subscribe(value => {
            this.formChanged = this.isChangedDictionary() || this.isChangedWords()
        })

        this.moveToAnotherDictionary = new MoveToAnotherDictionary("", [])
    }

    submitForm() {
        UpdateDictionaryAndRowsCommand.Update(this.newDictionary, this.formGroup.value)
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.model.updateDictionary(this.newDictionary).subscribe({
                next: (v) => {
                    this.oldDictionary = this.newDictionary
                    this.formChanged=false
                    this.messageService.reportMessage(new Message(v))
                },
                error: (e: HttpErrorResponse) => {
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

    redirectToCards(learnStatus:string){
        this.router.navigateByUrl(`cards/study/${this.newDictionary.UpdateDictionaryCommand?.Id}/${learnStatus}`)
    }

    changeEditMode(){
        this.editMode = !this.editMode
    }

    sort(mode:string){
        switch(mode){
            case "sortByLastLearnStatusChangedAtDesc":
                let sortedDesc = this.formGroup.value.DictionaryRows.sort((a:any,b:any)=> {return Date.parse(a.LearnStatusChangedAt.toString()) - Date.parse(b.LearnStatusChangedAt.toString())})
                this.formGroup.patchDictionaryRows(sortedDesc)
                break;
            case "sortByLastLearnStatusChangedAtAsc":
                let sortedAsc = this.formGroup.value.DictionaryRows.sort((a:any,b:any)=> {return Date.parse(b.LearnStatusChangedAt.toString()) - Date.parse(a.LearnStatusChangedAt.toString())})
                this.formGroup.patchDictionaryRows(sortedAsc)
                break;
        }
    }

    showLearnStatusPanel(){
        this.isShowLearnStatusPanel = !this.isShowLearnStatusPanel
    }

    laodImg(event: any) {
        const file = <File>event.target.files[0]
        if (file) {
            const reader = new FileReader();
            reader.onload = () => {
                let img = reader.result as string
                this.currentImg = img
            }
            reader.readAsDataURL(file)
        }
    }

    showNote(index:number){
        let element = document.getElementById(`showWordContext${index}`)

        let t =(element?.nextSibling as HTMLElement)
        t.style.display = "table-row"
        
    }

    closeNote(index:number){
        let element = document.getElementById(`showWordContext${index}`)

        let t =(element?.nextSibling as HTMLElement)
        t.style.display = "none"
    }

    changeLearnStatusChangedAt(row:GetDictionaryFormGroup){
        this.formGroup.changeLearnStatusChangedAt(row,new Date(new Date().toUTCString()))
    }

    showImg(img:string, event:any){
        this.currentShowImg = img
        let element = document.getElementById("showImg");
        element!.style.display = "block"
        element!.style.setProperty("border-radius","8px")
    }

    closeShowImg(img:string){
        let element = document.getElementById(`${"showImg"}`);
        element!.style.display = "none"
        this.currentShowImg = ""
    }

    saveImg() {
        this.formGroup.addImg(this.rowForImg, this.currentImg)
    }

    loadRowImg(rowIndex: number) {
        this.rowForImg = rowIndex
    }

    moveToAnotherDictionarySubmitForm() {
        this.moveToAnotherDictionaryFormSubmitted = true
        let model = new MoveToAnotherDicitonaryRowsCommand(this.moveToAnotherDictionary.dictionaryTargetId, this.moveToAnotherDictionary.WordsId)
        if (model.DictionaryTargetId && model.WordsId.length != 0) {
            this.model.moveToAnotherDictionaryDictionary(model).subscribe({
                next: (v) => {
                    this.messageService.reportMessage(new Message(v))
                },
                error: (e: HttpErrorResponse) => {
                    this.moveToAnotherDictionaryFormSubmitted = false
                },
                complete: () => {
                    this.moveToAnotherDictionaryFormSubmitted = false
                }
            })
        }
        else this.formSubmitted = false
    }

    selecteDictionary() {
        this.selectMode = !this.selectMode;
    }
    selecteDictionaryRowsStyle() {
        return this.selectMode ? { border: "1px solid red" } : {}
    }
    selecteDictionaryRowsClass() {
        return !this.selectMode ? {
            border: true,
            "border-secondary-subtle": true
        } : {}
    }
    selecteDictionaryRows(index: number, event: any) {
        if (!this.selectMode) return
        let htmlElement = event.target as HTMLElement

        let firstHtmlTD: HTMLElement
        if (htmlElement.parentElement?.nodeName == "TR") {
            firstHtmlTD = htmlElement.parentElement.childNodes[0] as HTMLElement
        }
        else {
            firstHtmlTD = htmlElement.parentElement?.parentElement?.childNodes[0] as HTMLElement
        }

        if (firstHtmlTD?.classList.contains('bg-danger')) {
            firstHtmlTD?.classList.replace('bg-danger', 'bg-success')
            if (firstHtmlTD.parentElement) {
                firstHtmlTD.parentElement.style.border = "1px solid green"
            }
        } else {
            firstHtmlTD?.classList.replace('bg-success', 'bg-danger')
            if (firstHtmlTD.parentElement) {
                firstHtmlTD.parentElement.style.border = "1px solid red"
            }
        }

        let row = this.newDictionary.UpdateDictionaryRowsCommand?.DictionaryRows[index]
        if (row?.Id) {
            {
                this.moveToAnotherDictionary.WordsId.push(row.Id)
            }
        }
    }

    addNewDictionaryRowsToMainFormGroup(newDicitonaryRows: FormArray) {
        this.formGroup.addNewRows(newDicitonaryRows)
    }

    showWordTranslation() {
        this.isShowWordTranslation = !this.isShowWordTranslation
    }

    generateWordContext(spanId: string, index: number, row: GetDictionaryFormGroup, word: string, store: string) {
        this.showLoadingSpan(index, spanId);

        let lastWordContextIndex = this.formGroup.addNewWordContext(row, index);

        this.aiService.getWordContextGeneratedObservable().subscribe(x => {
            this.formGroup.addWordContext(x, row, lastWordContextIndex - 1);
        })

        this.aiService.generateWordContext(new GenerateTextQuery(row.controls[word].value))

        this.aiService.getGeneratedWordContextObservable().subscribe(x => {
            this.showLoadingSpan(index, spanId, "none");
        })
    }

    showWordContext(id: string, index: number) {
        let element = document.getElementById(`${id}${index}`);
        if (element != null) {
            element.style.display = 'table-row'
        }
    }

    showLoadingSpan(index: number, id: string, displayValue: string = 'inline') {
        let element = document.getElementById(`${id}${index}`);
        if (element != null) {
            element.style.display = displayValue
        }
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

    getWordContexts(rowIndex: number, controllerName: string): FormArray<GetDictionaryFormGroup> {
        return this.formGroup.getWordContexts(rowIndex, controllerName) as FormArray<GetDictionaryFormGroup>
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
        return this.formGroup.value.NewDictionaryRows.length > 1 ||
            (Object.keys(this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows!).some(key =>
                this.formGroup.value.DictionaryRows[Number(key)].WordText != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows[Number(key)].WordText ||
                this.formGroup.value.DictionaryRows[Number(key)].LearnStatus != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows[Number(key)].LearnStatus ||
                this.formGroup.value.DictionaryRows[Number(key)].WordTranslation != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows[Number(key)].WordTranslation ||
                this.formGroup.value.DictionaryRows[Number(key)].WordContexts?.length != this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows[Number(key)].WordContexts?.length
            )
                ||
                this.oldDictionary.UpdateDictionaryRowsCommand?.DictionaryRows.length != this.formGroup.value.DictionaryRows.length
            )
    }

    isChangedDictionary(): boolean {
        return this.oldDictionary.UpdateDictionaryCommand?.Title != this.formGroup.value.Title ||
            this.oldDictionary.UpdateDictionaryCommand?.Description != this.formGroup.value.Description ||
            this.oldDictionary.UpdateDictionaryCommand?.WordLanguage != this.formGroup.value.WordLanguage ||
            this.oldDictionary.UpdateDictionaryCommand?.TranslationLanguage != this.formGroup.value.TranslationLanguage
    }

    private setNewLearnStatues() {
        let result = Array<{ index: number, value: string, key: string }>();
        Object
            .keys(LearnStatus)
            .forEach((v, i) => {
                if (isNaN(Number(v)) && i != 0 && i != 4) {
                    result.push({
                        index: i,
                        value: Object.values(LearnStatus)[i],
                        key: v
                    })
                }
            })
        this.newLearnStatues = result;
    }

    public setSelectedLearnStatues() {
        let result = Array<{ index: number, value: string, key: string }>();
        Object
            .keys(LearnStatus)
            .forEach((v, i) => {
                if (isNaN(Number(v)) && i != 0) {
                    result.push({
                        index: i,
                        value: Object.values(LearnStatus)[i],
                        key: v
                    })
                }
            })
        this.selectedLearnStatues = result;
    }
}