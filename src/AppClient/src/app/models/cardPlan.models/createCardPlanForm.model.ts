import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"
import { GetCardPlan } from "./CardPlan.model"
import { Dictionary } from "../dictionary.models/dictionary.model"

export class CreateCardPlanFormControl extends FormControl {
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

export class CreateCardPlanFormGroup extends FormGroup {
    constructor(private formBuilder:FormBuilder,private cardPlan?:GetCardPlan,private freeDictionaries?:Dictionary[]) {
        super({
            Title: new CreateCardPlanFormControl("Название словаря", "Title", cardPlan?.Title, Validators.required),

            Description: new CreateCardPlanFormControl("Описание словаря", "Description", cardPlan?.Description),
            
            StoredDictionariesId: 
            formBuilder.array((cardPlan?.Dictionaries.map(x=>({stored:true,selected:true,value:x}))??[]).concat(freeDictionaries?.map((x:Dictionary)=>({stored:false,selected:false,value:x}))??[]) ?? []),

            Id: new CreateCardPlanFormControl("Название словаря", "Id", cardPlan?.Id),

        })
    }

    get getControls(): CreateCardPlanFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as CreateCardPlanFormControl);
    }

    addDictionaryId(dictionaryId:Dictionary){
        let dictionariesId = this.controls["StoredDictionariesId"] as FormArray
        dictionariesId.push(this.formBuilder.control(dictionaryId))
    }

    deleteDictionaryId(dictionaryId:Dictionary){
        let dictionariesId = this.controls["StoredDictionariesId"] as FormArray
        dictionariesId.removeAt((dictionariesId.value as Array<Dictionary>).findIndex(x=>x.Id == dictionaryId.Id))
    }

    getDictionary(dictionaryId:any){
        let dictionariesId = this.controls["StoredDictionariesId"] as FormArray
        return dictionariesId.value[(dictionariesId.value as Array<Dictionary>).findIndex(x=>x.Id == dictionaryId.Id)]
    }
}