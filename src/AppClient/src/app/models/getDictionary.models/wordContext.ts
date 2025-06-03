export class WordContexsts{
    constructor(public Text:string, public IsForSave: boolean = false){}

    static Create(wordContests:string[]):WordContexsts[]{
        return wordContests?.map(element => {
            return new WordContexsts(element)
        })??[];
    }
}