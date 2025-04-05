import { Component } from "@angular/core"
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
        
        router.events.pipe(filter((e: any) => e instanceof NavigationEnd || e instanceof NavigationCancel))
            .subscribe(e => { 
                this.lastMessage = null
                this.isClosed = false
            });
    }

    closeBtn=() => {
        this.isClosed=true
        this.lastMessage = null
        this.isClosed=false
    }
}