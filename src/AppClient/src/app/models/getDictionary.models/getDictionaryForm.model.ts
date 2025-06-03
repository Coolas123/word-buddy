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
            let wordContexts = dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].WordContexts.map((x)=>this.formBuilder.group({
                Text: new GetDictionaryFormControl("Контекст", "Text", x),
            
                IsForSave: new GetDictionaryFormControl("Сохранить контекст", "IsForSave", true),
            }))??[]

            let wordContextsArray = formBuilder.array(wordContexts??[])

            r.push(this.formBuilder.group({
                //Id: new GetDictionaryFormControl("номер", "Id", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].Id),

                WordText: new GetDictionaryFormControl("Слово", "WordText", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].WordText),
    
                WordTranslation: new GetDictionaryFormControl("Перевод", "WordTranslation", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].WordTranslation),
                
                LearnStatus: new GetDictionaryFormControl("Статус изученности", "LearnStatus", Object.keys(LearnStatus).findIndex((x)=>(x == (dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i]?.LearnStatus??"")))),
                
                ImgBase64: new GetDictionaryFormControl("Фотография", "ImgBase64", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].ImgBase64 ? "data:image/jpeg;base64,"+dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].ImgBase64:""),

                ImgPath: new GetDictionaryFormControl("Фотография", "ImgPath", ""),
                
                NoteText: new GetDictionaryFormControl("Заметка", "NoteText", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].NoteText,Validators.maxLength(1024)),

                LearnStatusChangedAt: new GetDictionaryFormControl("Дата изменения", "LearnStatusChangedAt", dictionary.UpdateDictionaryRowsCommand?.DictionaryRows[i].LearnStatusChangedAt),

                WordContexts: wordContextsArray
            }))
        }

        let newRows = this.controls["NewDictionaryRows"] as FormArray
        newRows.push(this.formBuilder.group({
            NewWord: new GetDictionaryFormControl("Слово", "NewWord", ""),

            NewTranslation: new GetDictionaryFormControl("Перевод", "NewTranslation", ""),

            LearnStatus: new GetDictionaryFormControl("Статус изученности", "LearnStatus", 4),

            ImgBase64: new GetDictionaryFormControl("Фотография", "ImgBase64", ""),

            ImgPath: new GetDictionaryFormControl("Фотография", "ImgPath", ""),

            NoteText: new GetDictionaryFormControl("Заметка", "NoteText", "",Validators.maxLength(1024)),
            
            WordContexts: this.formBuilder.array([]),
        }))
    }

    get getDictionaryControls(): GetDictionaryFormControl[] {
        return Object.keys(this.controls)
            .map(k => this.controls[k] as GetDictionaryFormControl);
    }

    changeLearnStatusChangedAt(row:FormGroup, newDate:Date){
        row.controls["LearnStatusChangedAt"].patchValue(newDate)
    }

    patchDictionaryRows(rows:any){
        this.controls["DictionaryRows"].patchValue(rows);
    }

    addImg(rowIndex:number,img:any){
        (this.getRows().controls[rowIndex] as FormGroup).controls["ImgBase64"].patchValue(img)
    }
    addImgFile(rowIndex:number,img:any){
        (this.getRows().controls[rowIndex] as FormGroup).controls["ImgFile"].patchValue(img)
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

    getWordContexts(rowIndex:number, controllerName:string){
        let rows = this.controls[controllerName] as FormArray

        let row = rows.controls[rowIndex] as FormGroup

        return row.controls["WordContexts"] as FormArray
    }

    addNewWordContext(row:GetDictionaryFormGroup, rowIndex:number):number{
        let newWordContexts = this.formBuilder.group({
            Text: new GetDictionaryFormControl("Контекст", "Text", ""),
        
            IsForSave: new GetDictionaryFormControl("Сохранить контекст", "IsForSave", false),
        })
        let wordContexts = row.controls["WordContexts"] as FormArray

        wordContexts.push(newWordContexts);

        return wordContexts.length
    }

    addWordContext(wordContext: string,row:GetDictionaryFormGroup, wordContextIndex:number){
        let wordContexts = row.controls["WordContexts"] as FormArray
        let wordContextsGroup = wordContexts.controls[wordContextIndex] as FormGroup
        wordContextsGroup.controls["Text"].setValue(wordContextsGroup.controls["Text"].value+wordContext)
    }

    addNewRow(){
        let newRow = this.formBuilder.group({
            NewWord: new GetDictionaryFormControl("Слово", "NewWord", ""),

            NewTranslation: new GetDictionaryFormControl("Перевод", "NewTranslation", ""),

            LearnStatus: new GetDictionaryFormControl("Статус", "LearnStatus", 4),

            ImgBase64: new GetDictionaryFormControl("Фотография", "ImgBase64", ""),

            ImgPath: new GetDictionaryFormControl("Фотография", "ImgPath", ""),

            NoteText: new GetDictionaryFormControl("Заметка", "NoteText", "",Validators.maxLength(1024)),
            
            WordContexts: this.formBuilder.array([]),
        })
        this.getNewRows().push(newRow)
    }

    addNewRows(newRows:FormArray){
        let getNewRows = this.getNewRows()
       
        let lastRow = getNewRows.controls[getNewRows.value.length-1]
        lastRow.patchValue({NewWord:newRows.value[0].NewWord,NewTranslation:newRows.value[0].NewTranslation})
        
        for(let i=1;i<newRows.value.length;i++){
            let newRow = this.formBuilder.group({
                NewWord: new GetDictionaryFormControl("Слово", "NewWord", newRows.value[i].NewWord),
    
                NewTranslation: new GetDictionaryFormControl("Перевод", "NewTranslation", newRows.value[i].NewTranslation),
    
                LearnStatus: new GetDictionaryFormControl("Статус", "LearnStatus", 4),

                ImgBase64: new GetDictionaryFormControl("Фотография", "ImgBase64", ""),

                ImgPath: new GetDictionaryFormControl("Фотография", "ImgPath", ""),

                NoteText: new GetDictionaryFormControl("Заметка", "NoteText", "",Validators.maxLength(1024)),
                
                WordContexts: this.formBuilder.array([]),
            })
            getNewRows.push(newRow)
        }
        this.addNewRow()
    }
}