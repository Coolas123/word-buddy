import { Language } from "../../enums/language";
import { LearnStatus } from "../../enums/learnStatus";

export class DictionaryViewModel {
    constructor(
        public WordLanguage?: Language,
        public TranslationLanguage?: Language,
        public LastViewedAt?: Date,
        public Id:string="",
        public Title: string="",
        public Description: string="",
        public DictionaryRows: DictionaryRowViewModel[] =[]
    ) { }
}

export class DictionaryRowViewModel{
    constructor(
        public WordText:string,
        public WordTranslation:string,
        public LearnStatus: LearnStatus,
    ){}
}

export class SaveGeneratedTextHistory{
    constructor(public text:string){}
}

export class GetGeneratedTextHistory{
    constructor(public Text:string,public Id:string){}
}