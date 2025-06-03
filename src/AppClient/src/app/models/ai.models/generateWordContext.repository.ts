import { Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable } from "rxjs/internal/Observable"
import { ReplaySubject } from "rxjs"
import { MessageService } from "../alertMessage.models/alertMessage.service"
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { GenerateTextQuery } from "./GenerateTextQuery"

@Injectable({
    providedIn: 'root'
})
export class AiService {
    private _hubConnection: HubConnection
    private replaySubject: ReplaySubject<string>
    private wordContextGeneratedSubject: ReplaySubject<string>

    constructor(public dataSource: RestDataSource<GenerateTextQuery>, private messageService: MessageService) {
        this.replaySubject = new ReplaySubject<string>(1);
        this.wordContextGeneratedSubject = new ReplaySubject<string>(1);

        this._hubConnection = new HubConnectionBuilder()
            .withUrl(UserURLs.generateWordContextHubUri())
            .withAutomaticReconnect()
            .build();

        this._hubConnection.on("UpdateUsersAsync", users => {

        });

        this._hubConnection.on("SendMessageAsync", (user, message) => {
            this.replaySubject.next(message)
        });

        this._hubConnection.on("OnGeneratedWordContextAsync", (message) => {
            this.wordContextGeneratedSubject.next(message)
        });
    }

    getWordContextGeneratedObservable(): Observable<string> {
        let subject = new ReplaySubject<string>(1)
        this.replaySubject.subscribe(x => {
            subject.next(x)
        })
        return subject
    }

    getGeneratedWordContextObservable(): Observable<string> {
        let subject = new ReplaySubject<string>(1)
        this.wordContextGeneratedSubject.subscribe(x => {
            subject.next(x)
        })
        return subject
    }

    removeConnection() {
        this._hubConnection.invoke("RemoveConntextion", "anton").then(x => {
            console.log("disconntected")
        });
    }


    generateWordContext(generateTextQuery: GenerateTextQuery) {
        try {
            if (this._hubConnection.connectionId != undefined) {
                this._hubConnection.invoke("SendMessageAsync", "anton", generateTextQuery).then(x => {

                });
            }
            else this._hubConnection.start().then(x => {

                this._hubConnection.invoke("SendMessageAsync", "anton", generateTextQuery).then(x => {

                });

            })
                .catch(x => {
                    console.log(x)
                })
        }
        catch { }
    }
}