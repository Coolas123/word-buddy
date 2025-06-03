export class NewDictionaryRows{
    constructor(public newDictionaryRows:NewDictionaryRow[]){}
}

class NewDictionaryRow{
    constructor(public newWord:string,public newTranslation:string){}
}