export class MoveToAnotherDictionary{
    constructor(
        public dictionaryTargetId: string,
        public WordsId: string[]){}
}

export class MoveToAnotherDicitonaryRowsCommand{
    constructor(
        public DictionaryTargetId: string,
        public WordsId: string[]){}
}