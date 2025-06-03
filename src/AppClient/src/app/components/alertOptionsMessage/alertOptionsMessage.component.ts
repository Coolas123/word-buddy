import { Component } from "@angular/core"
import { Message } from "../../models/alertMessage.models/message.model";
import { Router, NavigationEnd, NavigationCancel } from "@angular/router";
import { filter } from 'rxjs/operators';
import { MessageOptionsService } from "../../models/alertOptionsMessage.models/alertOptionsMessage.service";

@Component({
    selector: "alertOptionsMessage",
    templateUrl: "./alertOptionsMessage.component.html",
    providers: []
})

export class AlertOptionsMessageComponent {
    lastMessage?: Message | null;
    isClosed?: boolean = false

    constructor(private messageOptionsService: MessageOptionsService, router: Router) {
        this.messageOptionsService.messages.subscribe(message =>{
            this.lastMessage = message
        })
        router.events.pipe(filter((e: any) => e instanceof NavigationEnd || e instanceof NavigationCancel))
            .subscribe(e => {
                this.lastMessage = null
                this.isClosed = false
            });
            
    }

    YesBtn() {
        this.messageOptionsService.subjectAnswer?.next(false)
        this.complete()
    }


    NoBtn() {
        this.messageOptionsService.subjectAnswer?.next(true)
        this.complete()
    }

    closeBtn(){
        this.complete()
    }

    complete() {
       // this.isClosed = true
        this.lastMessage = null
        //this.isClosed = false
        this.messageOptionsService.clearMessages()
        //this.messageOptionsService.messages.unsubscribe()
        //this.messageOptionsService.messages.subscribe(message => this.lastMessage = message)
    }
}