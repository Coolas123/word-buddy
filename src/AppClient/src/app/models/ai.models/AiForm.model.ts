import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"

export class AiFormGroupFormControl extends FormControl {
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

export class AiFormGroup extends FormGroup {
    constructor(private formBuilder:FormBuilder) {
        super({
            // Title: new CreateCardPlanFormControl("Название словаря", "Title", cardPlan?.Title, Validators.required),

            // Description: new CreateCardPlanFormControl("Описание словаря", "Description", cardPlan?.Description),
            
            // StoredDictionariesId: 
            // formBuilder.array((cardPlan?.Dictionaries.map(x=>({stored:true,selected:true,value:x}))??[]).concat(freeDictionaries?.map((x:Dictionary)=>({stored:false,selected:false,value:x}))??[]) ?? []),

            // Id: new CreateCardPlanFormControl("Название словаря", "Id", cardPlan?.Id),

        })
    }

    get getControls(): AiFormGroupFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as AiFormGroupFormControl);
    }
}