import { inject } from "@angular/core"
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"

export class CreateDictionaryFormControl extends FormControl {
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

export class CreateDictionaryFormGroup extends FormGroup {
    constructor(private formBuilder:FormBuilder) {
        super({
            Title: new CreateDictionaryFormControl("Название словаря", "Title", "", Validators.required),

            Description: new CreateDictionaryFormControl("Описание словаря", "Description", ""),

            WordLanguage: new CreateDictionaryFormControl("Язык слова", "WordLanguage", "", Validators.required),

            TranslationLanguage: new CreateDictionaryFormControl("Язык перевода", "TranslationLanguage", "", Validators.required),

            Rows: formBuilder.array([])
        })
        
        let r = this.controls["Rows"] as FormArray
        r.push(this.formBuilder.group({
            Word: new CreateDictionaryFormControl("Слово", "Word", ""),

            Translation: new CreateDictionaryFormControl("Перевод", "Translation", ""),
        }))
    }

    get createDictionaryControls(): CreateDictionaryFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as CreateDictionaryFormControl);
    }

    getFormValidationMessages(): string[] {
        let messages: string[] = [];
        this.createDictionaryControls.forEach(c =>c.getValidationMessages().forEach(m => messages.push(m)));
        return messages;
    }

    getRows(){
        return this.controls["Rows"] as FormArray
    }

    addRow(){
        let newRow = this.formBuilder.group({
            Word: new CreateDictionaryFormControl("Слово", "Word", ""),

            Translation: new CreateDictionaryFormControl("Перевод", "Translation", ""),
        })
        this.getRows().push(newRow)
    }
}