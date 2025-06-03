import { Component, EventEmitter, Output } from "@angular/core";
import { RouterModule } from "@angular/router";
import { Subject } from "rxjs";
import { SelectUploadFileFormControl, SelectUploadFileFormGroup } from "../../../models/selectUploadFile.models/selectUploadFile.From.model";
import { AbstractControl, FormArray, FormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MessageService } from "../../../models/alertMessage.models/alertMessage.service";
import { Message } from "../../../models/alertMessage.models/message.model";


@Component({
    selector: "selectUploadFile",
    templateUrl: "./selectUploadFile.component.html",
    imports: [FormsModule, ReactiveFormsModule,RouterModule]
})
export class SelectUploadFileComponent{
    formSubmitted: boolean = false
    formGroup: SelectUploadFileFormGroup
    uploadedFileSubject = new Subject<string>()

    constructor(private formBuilder:FormBuilder, private messageService: MessageService){
        this.formGroup = new SelectUploadFileFormGroup(formBuilder)

        this.uploadedFileSubject.subscribe(async fileText=>{
            let splitedWords:string[][] = await this.createNewDictionaryRows(
                fileText,
                this.formGroup.getControl("WordSeparator").value,
                this.formGroup.getControl("LineSeparator").value
            )
            this.formGroup.addNewRows(splitedWords)
        })
    }

    @Output() onNewDictionaryRowsFromFile = new EventEmitter<FormArray>();
    addNewDictionaryRowsToMainFormGroup(newDictionaryRows:FormArray) {
        this.onNewDictionaryRowsFromFile.emit(newDictionaryRows);
    }

    selectUploadFileSubmitForm(){
        this.formSubmitted = true
        if (this.formGroup.valid) {
            this.addNewDictionaryRowsToMainFormGroup(this.formGroup.getNewRows())

            document.getElementById("closeModalButton")?.click();
            
            this.messageService.reportMessage(new Message("Слова загружены"))

            this.formGroup = new SelectUploadFileFormGroup(this.formBuilder)
            this.formSubmitted = false
        }
    }

    async selectUploadFile(event:Event){
        let files = (event.target as HTMLInputElement).files ??[]
        var reader = new FileReader()
        reader.onload = async(e: any) => {
            this.uploadedFileSubject.next(e.target.result)
        }
        await reader.readAsText(files[0])
    }

    showFormControl(control:SelectUploadFileFormControl){
        control.disabled ? control.enable() : control.disable();
    }

    getNewRows(){
        return this.formGroup.getNewRows()
    }

    getControl(row: AbstractControl): SelectUploadFileFormControl {
        return row as SelectUploadFileFormControl
    }

    private async createNewDictionaryRows(rowsString:string, wordSeparator:string, lineSeparator:string):Promise<string[][]>{
        return new Promise((resolve)=>{
            let splitedWords = rowsString.split(lineSeparator ==""?/\r\n|\n|\r/:lineSeparator).map(element => {
                return element.split(wordSeparator).map(x=>x.trim())
            })
            resolve(splitedWords)
        })

    }
}