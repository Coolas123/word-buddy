import { Language } from "../../enums/language";


export class Dictionary {
    constructor(
        public WordLanguage?: Language,
        public TranslationLanguage?: Language,
        public LastViewedAt?: Date,
        public Id:string="",
        public Title: string="",
        public Description: string="",
    ) { }
}