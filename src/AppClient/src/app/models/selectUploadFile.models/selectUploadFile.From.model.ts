import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"

export class SelectUploadFileFormControl extends FormControl {
    label: string
    modelProperty: string

    constructor(label: string, property: string, value: any, validator?: any) {
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
                }
            }
        }
        
        return messages
    }
}

export class SelectUploadFileFormGroup extends FormGroup {
    constructor(private formBuilder:FormBuilder) {
        let lineSeparator = new SelectUploadFileFormControl("Разделитель строк", "LineSeparator", "", 
            Validators.compose([Validators.required]))
        lineSeparator.disable()

        super({
            WordSeparator: new SelectUploadFileFormControl("Разделитель слов", "WordSeparator", "", Validators.required),

            LineSeparator: lineSeparator,

            File: new SelectUploadFileFormControl("Файл", "File", "",  
                Validators.compose([Validators.required])),
            
            NewDictionaryRows: formBuilder.array([]),
        }
    )
    }

    get getSelectUploadFileControls(): SelectUploadFileFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as SelectUploadFileFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.getSelectUploadFileControls.forEach(c => c.getValidationMessages()
            .forEach(m => messages.push(m)));
        return messages;
    }

    getNewRows(){
        return this.controls["NewDictionaryRows"] as FormArray<FormGroup>
    }

    getControl(controlName:string){
        return this.controls[controlName] as SelectUploadFileFormControl
    }

    addNewRows(newRows:string[][]){
        for(let i=0; i<newRows.length;i++){
            let newRow = this.formBuilder.group({
                NewWord: new SelectUploadFileFormControl("Слово", "NewWord", newRows[i][0]),
    
                NewTranslation: new SelectUploadFileFormControl("Перевод", "NewTranslation", newRows[i][1])
            })
            this.getNewRows().push(newRow)
        }
        }
        
}