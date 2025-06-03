import { Component, OnDestroy } from "@angular/core"
import { MessageService } from "../../models/alertMessage.models/alertMessage.service"
import { Message } from "../../models/alertMessage.models/message.model";
import { Router, NavigationEnd, NavigationCancel } from "@angular/router";
import { filter } from 'rxjs/operators';

@Component({
    selector: "alertMessage",
    templateUrl: "./alertMessage.component.html",
    providers: []
})

export class AlertMessageComponent {
    lastMessage?: Message | null;
    isClosed?:boolean=false

    constructor(private messageService: MessageService, router: Router) {
        this.messageService.messages.subscribe(message => this.lastMessage = message)

        router.events.subscribe(e =>{ 
            if (e instanceof NavigationEnd || e instanceof NavigationCancel){
                this.lastMessage = null
                this.isClosed = false
            }
            });
    }

    closeBtn=() => {
        // this.isClosed=true
        this.lastMessage = null
        // this.isClosed=false
        this.messageService.clearMessages()
        //this.messageService.messages.unsubscribe()
        // console.log(this.lastMessage)
        //this.messageService.messages.subscribe(message => this.lastMessage = message)
        // console.log(this.lastMessage)
    }
}