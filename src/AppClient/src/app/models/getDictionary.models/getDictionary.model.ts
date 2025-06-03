import { Language } from "../../enums/language"
import { LearnStatus } from "../../enums/learnStatus"
import { WordContexsts } from "./wordContext"

export class UpdateDictionaryAndRowsCommand {
    constructor(
        public UpdateDictionaryCommand?: UpdateDictionaryCommand,
        public UpdateDictionaryRowsCommand?: UpdateDictionaryRowsCommand,
        public CreateDictionaryRowsCommand?: CreateDictionaryRowsCommand
    ) {}

    static Update(newDictionary:UpdateDictionaryAndRowsCommand,formDictionary: any){
        newDictionary.UpdateDictionaryCommand!.Title = formDictionary.Title,
        newDictionary.UpdateDictionaryCommand!.Description = formDictionary.Description
        newDictionary.UpdateDictionaryCommand!.TranslationLanguage = formDictionary.TranslationLanguage
        newDictionary.UpdateDictionaryCommand!.WordLanguage = formDictionary.WordLanguage

        newDictionary.UpdateDictionaryRowsCommand?.DictionaryRows.forEach((value, index) => {
            value.WordText = formDictionary.DictionaryRows[index].WordText
            value.WordTranslation = formDictionary.DictionaryRows[index].WordTranslation
            value.LearnStatus = formDictionary.DictionaryRows[index].LearnStatus
            value.WordContexts = formDictionary.DictionaryRows[index].WordContexts.filter((x:WordContexsts)=>x.IsForSave).map((x:WordContexsts)=>x.Text)??[]
            value.ImgBase64 = formDictionary.DictionaryRows[index].ImgBase64
            value.NoteText = formDictionary.DictionaryRows[index].NoteText
            value.LearnStatusChangedAt = formDictionary.DictionaryRows[index].LearnStatusChangedAt
        })
        
        let createRows:CreateDictionaryRowCommand[] = []
        formDictionary.NewDictionaryRows.forEach((value:any) => {
            if(!value.NewWord || !value.NewTranslation) return
                    createRows.push(new CreateDictionaryRowCommand(
                        value.NewWord,
                        value.LearnStatus,
                        value.NewTranslation,
                        value.WordContexts?.filter((x:WordContexsts)=>x.IsForSave).map((x:WordContexsts)=>x.Text),
                        value.ImgBase64,
                        value.NoteText
                    ))
        });
        newDictionary.CreateDictionaryRowsCommand!.DictionaryRows = createRows
    }
}

class UpdateDictionaryCommand {
    constructor(
        public WordLanguage: Language,
        public TranslationLanguage: Language,
        public LastViewedAt: Date,
        public Id: string,
        public Title: string,
        public Description: string,
    ) { }
}

export class UpdateDictionaryRowsCommand {
    constructor(
        public DictionaryRows: UpdateDictionaryRowCommand[],
        public DictionaryId?: string
    ) { }
}

export class UpdateDictionaryRowCommand {
    constructor(
        public Id: string,
        public WordText: string,
        public LearnStatus: LearnStatus,
        public LearnStatusChangedAt: Date,
        public CreatedAt: Date,
        public WordTranslation: string,
        public WordContexts: string[],
        public ImgBase64? :string,
        public ImgPath? :string,
        public NoteText?: string
    ) { }
}

class CreateDictionaryRowsCommand {
    constructor(
        public DictionaryRows: CreateDictionaryRowCommand[],
        public DictionaryId: string
    ) { }
}

class CreateDictionaryRowCommand {
    constructor(
        public WordText: string,
        public LearnStatus: LearnStatus,
        public WordTranslation: string,
        public WordContexts: string[],
        public ImgBase64? : string,
        public ImgPath? :string,
        public NoteText?: string
    ) { }
}