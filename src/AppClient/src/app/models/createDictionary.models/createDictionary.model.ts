import { Language } from "../../enums/language"
import { LearnStatus } from "../../enums/learnStatus"

class CreateDictionaryRowCommand {
    constructor(
        public WordText?: string,
        public LearnStatus?: LearnStatus,
        public WordTranslation?: string
    ) { }
}

class CreateDictionaryRowsCommand {
    constructor(
        public DictionaryRows?: CreateDictionaryRowCommand[],
    ) { }
}

class CreateDictionaryCommand {
    constructor(
        public WordLanguage?: Language,
        public TranslationLanguage?: Language,
        public Title: string = "",
        public Description: string = "",
    ) { }
}

export class CreateDictionaryAndRowsCommand{
    constructor(
        public CreateDictionaryRowsCommand?: CreateDictionaryRowsCommand,
        public CreateDictionaryCommand?: CreateDictionaryCommand
    ){}

    static create(newDictionary:any){
        let createRows = new CreateDictionaryRowsCommand([])
        console.log( newDictionary.Rows)
        for(let i=0; i< newDictionary.Rows.length; i++){
            if(!newDictionary.Rows[i].Word || !newDictionary.Rows[i].Translation) continue
            
            createRows.DictionaryRows?.push(new CreateDictionaryRowCommand(
                newDictionary.Rows[i].Word,
                newDictionary.Rows[i].LearnStatus,
                newDictionary.Rows[i].Translation
            ))
        }
        
        return new CreateDictionaryAndRowsCommand(createRows, new CreateDictionaryCommand(
            newDictionary.WordLanguage,
            newDictionary.TranslationLanguage,
            newDictionary.Title,
            newDictionary.Description
        ))
    }
}