import { Injectable } from "@angular/core";

import { Subject } from "rxjs";
import { Message } from "../alertMessage.models/message.model";

@Injectable({
    providedIn: 'root'
})
export class MessageOptionsService {
    private subject = new Subject<Message>()
    public subjectAnswer?:Subject<boolean>

    reportMessage(msg: Message,subject:Subject<boolean>) {
        if(msg.text == undefined) return
        this.subjectAnswer = subject
        this.subject.next(msg)
    }

    get messages(): Subject<Message> {
        return this.subject
    }

    clearMessages(){
        this.subject = new Subject<Message>()
    }
}