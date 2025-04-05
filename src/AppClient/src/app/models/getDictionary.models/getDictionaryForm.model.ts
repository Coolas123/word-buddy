import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"
import { UpdateDictionaryAndRowsCommand } from "./getDictionary.model"
import { LearnStatus } from "../../enums/learnStatus"

export class GetDictionaryFormControl extends FormControl {
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

export class GetDictionaryFormGroup extends FormGroup {
    constructor(private formBuilder:FormBuilder, dictionary: UpdateDictionaryAndRowsCommand) {
        super({
            Title: new GetDictionaryFormControl("Название словаря", "Title", dictionary.UpdateDictionaryCommand?.Title, Validators.required),

            Description: new GetDictionaryFormControl("Описание словаря", "Description", dictionary.UpdateDictionaryCommand?.Description),

            WordLanguage: new GetDictionaryFormControl("Язык слова", "WordLanguage", dictionary.UpdateDictionaryCommand?.WordLanguage, Validators.required),

            TranslationLanguage: new GetDictionaryFormControl("Язык перевода", "TranslationLanguage", dictionary.UpdateDictionaryCommand?.TranslationLanguage, Validators.required),

            DictionaryRows: formBuilder.array([]),

            NewDictionaryRows: formBuilder.array([])
        })
        
        let r = this.controls["DictionaryRows"] as FormArray
        for(let i=0; dictionary.UpdateDictionaryRowsCommand?.DictionaryRows && i<dictionary.UpdateDictionaryRowsCommand.DictionaryRows.length; i++){
            r.push(this.formBuilder.group({
                WordText: new GetDictionaryFormControl("Слово", "WordText", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].WordText),
    
                WordTranslation: new GetDictionaryFormControl("Перевод", "WordTranslation", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].WordTranslation),
                
                LearnStatus: new GetDictionaryFormControl("Статус", "LearnStatus", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].LearnStatus),
            }))
        }

        let newRows = this.controls["NewDictionaryRows"] as FormArray
        newRows.controls.push(this.formBuilder.group({
            NewWord: new GetDictionaryFormControl("Слово", "NewWord", ""),

            NewTranslation: new GetDictionaryFormControl("Перевод", "NewTranslation", ""),
        }))
    }

    get getDictionaryControls(): GetDictionaryFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as GetDictionaryFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.getDictionaryControls.forEach(c =>c.getValidationMessages().forEach(m => messages.push(m)));
        return messages;
    }

    getRows(){
        return this.controls["DictionaryRows"] as FormArray
    }

    getNewRows(){
        return this.controls["NewDictionaryRows"] as FormArray
    }

    addNewRow(){
        let newRow = this.formBuilder.group({
            NewWord: new GetDictionaryFormControl("Слово", "NewWord", ""),

            NewTranslation: new GetDictionaryFormControl("Перевод", "NewTranslation", ""),
        })
        this.getNewRows().push(newRow)
    }
}