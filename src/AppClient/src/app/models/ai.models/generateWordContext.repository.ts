import { Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable } from "rxjs/internal/Observable"
import { ReplaySubject } from "rxjs"
import { HttpErrorResponse } from "@angular/common/http"
import { MessageService } from "../alertMessage.models/alertMessage.service"
import { Message } from "../alertMessage.models/message.model"
import { GenerateTextContext } from "./generateWordContext.model"
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Injectable({
    providedIn: 'root'
})
export class AiService {
    private _hubConnection: HubConnection
    private replaySubject: ReplaySubject<string>
    
    constructor(public dataSource: RestDataSource<GenerateTextContext>, private messageService: MessageService) {
        this.replaySubject = new ReplaySubject<string>(1);
        
        this._hubConnection = new HubConnectionBuilder()
        .withUrl(UserURLs.generateWordContextHubUri())
        .withAutomaticReconnect()
        .build();

        this._hubConnection.on("UpdateUsersAsync", users => {

        });

        this._hubConnection.on("SendMessageAsync", (user, message) => {
            this.replaySubject.next(message)
        });
    }

    getGeneratedWordContextObservable(): Observable<string>{
        let subject = new ReplaySubject<string>(1)
        this.replaySubject.subscribe(x=>{
            subject.next(x)
        })
        return subject
    }

    generateWordContext(GenerateTextModel: GenerateTextContext){
        try{
            this._hubConnection.start().then(x=>{

                this._hubConnection.invoke("SendMessageAsync", "anton","2+2 is?").then(x=>{
                    
                });
                
            })
            .catch(x=>{
                console.log(x)
            })  
        }
        catch{}
    }
}