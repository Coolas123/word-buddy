import { Subject } from "rxjs";

export class Message {
    constructor(public text: string,
    public subject:Subject<boolean>) { }
   }