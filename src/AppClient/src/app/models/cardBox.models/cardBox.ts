import { LearnStatus } from "../../enums/learnStatus";

export class CardBox{
    constructor(
        public Id: string,
        public TotalWords:number,
        public LearnStatus: LearnStatus,
        public TotalWordsCountedAt: Date,
        public Period: number,
        public ViewedAt: Date
    ){}
}