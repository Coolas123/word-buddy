import { Injectable } from "@angular/core";
import { Message } from "./message.model";
import { Observable, Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    private subject = new Subject<Message>()

    reportMessage(msg: Message) {
        if(msg.text == undefined) return
        this.subject.next(msg)
    }

    get messages(): Observable<Message> {
        return this.subject
    }
}