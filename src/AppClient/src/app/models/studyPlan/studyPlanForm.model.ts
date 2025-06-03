// import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"

// export class StudyPlanFormControl extends FormControl {
//     label: string
//     modelProperty: string

//     constructor(label: string, property: string, value: any, validator?: any) {
//         super(value, validator)
//         this.label = label
//         this.modelProperty = property
//     }

//     getValidationMessages() {
//         let messages: string[] = []
//         if (this.errors) {
//             for (let errorName in this.errors) {
//                 switch (errorName) {
//                     case "required":
//                         messages.push("requered!!!");
//                         break;
//                     case "email":
//                         messages.push("email is worng!!!");
//                         break;
//                     case "serverError":
//                         messages.push(this.errors[errorName]);
//                         break;
//                 }
//             }
//         }
//         return messages
//     }
// }

// export class StudyPlanFormGroup extends FormGroup {
//     constructor(formBuilder:FormBuilder) {
//         super({
//             NotStudiedDictionariesId: formBuilder.array([]),
//             NotStudiedSelected: new StudyPlanFormControl("Выбрано", "selected", false, Validators.required),
            
//             InStudyingDictionariesId: formBuilder.array([]),
//             InStudyingSelected: new StudyPlanFormControl("Выбрано", "selected", false, Validators.required),

//             NeedToRememberDictionariesId: formBuilder.array([]),
//             NeedToRememberSelected: new StudyPlanFormControl("Выбрано", "selected", false, Validators.required),
            
//             VeryDifficultDictionariesId: formBuilder.array([]),
//             VeryDifficultSelected: new StudyPlanFormControl("Выбрано", "selected", false, Validators.required),
//         })
//     }

//     get getStudyPlanControls(): StudyPlanFormControl[] {
//         return Object.keys(this.controls)
//             .map(k => this.controls[k] as StudyPlanFormControl);
//     }
//     get getStudyPlanArrays(): FormArray[]{
//         return Object.keys(this.controls)
//             .map(k => {
//                 if(this.controls[k] as FormArray<any>){
//                     return this.controls[k] as FormArray<any>
//                 } 
//                 return undefined
//         }) ?? []
//     }

//     getFormValidationMessages(): string[] {
//         let messages: string[] = [];
//         this.getStudyPlanControls.forEach(c =>c.getValidationMessages().forEach(m => messages.push(m)));
//         return messages;
//     }

//     // getDictionariesId(){
//     //     return this.controls["TargetDictionariesId"] as FormArray
//     // }

//     // addDictionariesId(dictionariesId:string[]){
//     //     dictionariesId.forEach(element=>{
//     //         this.getDictionariesId().push(new StudyPlanFormControl("id сдщваря", "Id", element))
//     //     })
//     // }
// }