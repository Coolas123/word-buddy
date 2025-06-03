import { CardBox } from "../cardBox.models/cardBox";
import { Dictionary } from "../dictionary.models/dictionary.model";
 
export class CreateCardPlan{
    constructor(
        public Title:string,
        public Description:string,
        public DictionariesId:string[]
    ){}
}

export class GetCardPlan{
    constructor(
        public Id:string,
        public Title:string,
        public Description:string,
        public Dictionaries:Dictionary[],
        public CardBoxes: CardBox[],
        public ViewedAt:Date
    ){}
}

export class UpdateCardPlanViewModel{
    constructor(
        public Id:string,
        public Title:string,
        public Description?:string,
        public NewDictionariesId?:string[],
        public DeleteDictionariesId?:string[]
    ){}
}