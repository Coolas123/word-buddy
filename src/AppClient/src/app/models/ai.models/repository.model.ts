import { Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable } from "rxjs/internal/Observable"
import { ReplaySubject } from "rxjs"
import { HttpErrorResponse } from "@angular/common/http"
import { MessageService } from "../alertMessage.models/alertMessage.service"
import { Message } from "../alertMessage.models/message.model"
import { Dictionary } from "../dictionary.models/dictionary.model"
import { DictionaryViewModel, GetGeneratedTextHistory, SaveGeneratedTextHistory } from "./ai.model"

@Injectable({
    providedIn: 'root'
})
export class Model {
    dictionaries: Dictionary[] = []
    private getDictionariesReplaySubject: ReplaySubject<Dictionary[]>;
    public selectedDictionary?: DictionaryViewModel
    private getDictionaryReplaySubject: ReplaySubject<Dictionary | undefined>;
    savedContexts: GetGeneratedTextHistory[] = []

    constructor(public dataSource: RestDataSource<any>, private messageService: MessageService) {
        this.getDictionariesReplaySubject = new ReplaySubject<Dictionary[]>(1)
        this.getDictionaryReplaySubject = new ReplaySubject<Dictionary | undefined>(1)


        this.dataSource.getData(UserURLs.dictionariesURI()).subscribe(
            {
                next: (dictionaries) => {
                    this.dictionaries = dictionaries
                    this.getDictionariesReplaySubject.next(dictionaries)
                },
                error: (e: HttpErrorResponse) => {
                    this.messageService.reportMessage(new Message(e.error))
                    this.getDictionariesReplaySubject.next([])
                }
            })
    }

    loadDictionary(id: string) {
        this.dataSource.getData(UserURLs.getDictionaryViewModelWithRowsViewModel(id)).subscribe(
            {
                next: (selectedDictionary) => {
                    this.selectedDictionary = selectedDictionary
                    this.getDictionaryReplaySubject.next(selectedDictionary)
                },
                error: (e: HttpErrorResponse) => {
                    this.messageService.reportMessage(new Message(e.error))
                    this.getDictionaryReplaySubject.next(this.selectedDictionary)
                }
            })
    }

    saveContext(text:string){
        this.dataSource.saveData(UserURLs.saveGeneratedTextHistory(),new SaveGeneratedTextHistory(text)).subscribe(
            {
                next: (message) => {
                    this.messageService.reportMessage(new Message(message))
                },
                error: (e: HttpErrorResponse) => {
                    this.messageService.reportMessage(new Message(e.error))
                }
            })
    }

    loadSavedContexts() {
        this.dataSource.getData(UserURLs.getGeneratedTextHistories()).subscribe(
            {
                next: (contexts) => {
                    this.savedContexts=contexts
                },
                error: (e: HttpErrorResponse) => {
                    this.messageService.reportMessage(new Message(e.error))
                }
            })
    }

    public getDictionaries(): Dictionary[] {
        return this.dictionaries
    }

    public getDictionariesObservable(): Observable<Dictionary[]> {
        let subject = new ReplaySubject<Dictionary[]>(1)
        this.getDictionariesReplaySubject.subscribe(dictionaries => {
            subject.next(dictionaries)
        })
        return subject
    }

    public getDictionaryObservable(): Observable<Dictionary | undefined> {
        let subject = new ReplaySubject<Dictionary | undefined>(1)
        this.getDictionaryReplaySubject.subscribe(dictionary => {
            subject.next(dictionary)
        })
        return subject
    }
}