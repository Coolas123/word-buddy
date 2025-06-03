import { Injectable } from "@angular/core";
import { Message } from "./message.model";
import { Observable, ReplaySubject, Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    // private subject = new Subject<Message>()

    // reportMessage(msg: Message) {
    //     if(msg.text == undefined) return
    //     this.subject.next(msg)
    // }

    // get messages(): Subject<Message> {
    //     return this.subject
    // }

    clearMessages(){
        this.messages = new Subject<Message>();
    }

    messages: Subject<Message> = new Subject<Message>();

    reportMessage(msg: Message) {
        (this.messages as Subject<Message>).next(msg);
    }
}