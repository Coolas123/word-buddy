import { LearnStatus } from "../../enums/learnStatus";
import { CardBox } from "../cardBox.models/cardBox";

export class UpdateDictionaryRowsLearnStatusCommand{
    constructor(public DictionaryRows:UpdateDictionaryRowLearnStatusCommand[]){}
}

export class UpdateDictionaryRowLearnStatusCommand{
    constructor(
        public DictionaryRowId:string,
        public NewLearnStatus:string,
        public IsCardPlan : boolean
    ){}
}

export class CardPlanByCardBoxViewModel{
    constructor(
        public CardPlanId?:string,
        public CardBoxDictionariesId?:string[],
        public CardBox? : CardBox
    ){}
}