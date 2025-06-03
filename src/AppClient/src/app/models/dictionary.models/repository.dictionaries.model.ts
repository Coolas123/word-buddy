import { Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Dictionary } from "./dictionary.model"
import { Observable } from "rxjs/internal/Observable"
import { ReplaySubject } from "rxjs"
import { HttpErrorResponse } from "@angular/common/http"
import { MessageService } from "../alertMessage.models/alertMessage.service"
import { Message } from "../alertMessage.models/message.model"
import { CreateDictionaryAndRowsCommand } from "../createDictionary.models/createDictionary.model"

@Injectable({
    providedIn: 'root'
})
export class Model {
    dictionaries: Dictionary[] = []
    private replaySubject: ReplaySubject<Dictionary[]>;

    constructor(public dataSource: RestDataSource<any>, private messageService: MessageService) {
        this.replaySubject = new ReplaySubject<Dictionary[]>(1)
    }

    loadDIctionaries(){
        this.dataSource.getData(UserURLs.dictionariesURI()).subscribe(
            {
            next: (dictionaries) => {
                this.dictionaries = dictionaries
                this.replaySubject.next(dictionaries)
            },
            error: (e: HttpErrorResponse) => {
                this.messageService.reportMessage(new Message(e.error))
                this.replaySubject.next(this.dictionaries)
            }
        })
    }

    public getDictionaries(): Dictionary[] {
        return this.dictionaries
    }

    public getDictionariesObservable(): Observable<Dictionary[]> {
        let subject = new ReplaySubject<Dictionary[]>(1)
        this.replaySubject.subscribe(dictionaries=>{
            subject.next(dictionaries)
        })
        return subject
    }

    public saveDictionary(newDictionary: CreateDictionaryAndRowsCommand): Observable<any> {
        return this.dataSource.saveData(UserURLs.createDictionaryURI(),newDictionary)
    }
}