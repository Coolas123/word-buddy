import { Language } from "../../enums/language"
import { LearnStatus } from "../../enums/learnStatus"

export class UpdateDictionaryAndRowsCommand {
    constructor(
        public UpdateDictionaryCommand?: UpdateDictionaryCommand,
        public UpdateDictionaryRowsCommand?: UpdateDictionaryRowsCommand,
        public CreateDictionaryRowsCommand?: CreateDictionaryRowsCommand
    ) { }

    Update(formDictionary: any) {
        this.UpdateDictionaryCommand!.Title = formDictionary.Title,
            this.UpdateDictionaryCommand!.Description = formDictionary.Description
        this.UpdateDictionaryCommand!.TranslationLanguage = formDictionary.TranslationLanguage
        this.UpdateDictionaryCommand!.WordLanguage = formDictionary.WordLanguage

        this.UpdateDictionaryRowsCommand!.DictionaryRows.forEach((value, index) => {
            value.WordText = formDictionary.Rows[index].Word
            value.WordTranslation = formDictionary.Rows[index].Translation
            value.LearnStatus = formDictionary.Rows[index].LearnStatus
        })
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

class UpdateDictionaryRowsCommand {
    constructor(
        public DictionaryRows: UpdateDictionaryRowCommand[],
        public DictionaryId?: string
    ) { }
}

class UpdateDictionaryRowCommand {
    constructor(
        public Id: string,
        public WordText: string,
        public LearnStatus: LearnStatus,
        public LearnStatusChangedAt: Date,
        public CreatedAt: Date,
        public WordTranslation: string,
        public WordContext: string[]
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
        public WordTranslation: string
    ) { }
}