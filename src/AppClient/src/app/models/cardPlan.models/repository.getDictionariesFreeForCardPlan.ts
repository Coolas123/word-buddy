import { Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable, ReplaySubject } from "rxjs"
import { Dictionary } from "../dictionary.models/dictionary.model"
import { CreateCardPlan, GetCardPlan, UpdateCardPlanViewModel } from "./CardPlan.model"
import { HttpErrorResponse } from "@angular/common/http"
import { MessageService } from "../alertMessage.models/alertMessage.service"
import { Message } from "../alertMessage.models/message.model"

@Injectable({
    providedIn: 'root'
})
export class Model {
    public dictionaresFreeForCardPlanSubject: ReplaySubject<string>
    private replaySubject: ReplaySubject<Dictionary[]>
    private getCardPlansSubject: ReplaySubject<GetCardPlan[]>
    public dictionariesFreeForCardPlan:Dictionary[]=[]
    public cardPlans:GetCardPlan[]= []

    constructor(public dataSource: RestDataSource<any>, private messageService:MessageService) {
        this.replaySubject = new ReplaySubject<Dictionary[]>(1)
        this.getCardPlansSubject = new ReplaySubject<GetCardPlan[]>(1)
        this.dictionaresFreeForCardPlanSubject= new ReplaySubject<string>(1)

        this.dictionaresFreeForCardPlanSubject.subscribe(id=>{
            this.dataSource.getData(UserURLs.getFreeDictionariesForCardPlan()).subscribe({
                next:(x=>{
                this.dictionariesFreeForCardPlan = x
                this.replaySubject.next(x)
                }),
                error:(x=>{
                    this.replaySubject.next([])
                })
            })
        })
    }

    public loadCardPlans(){
        this.dataSource.getData(UserURLs.getCardPlanWithDictionariesAndCardBoxes()).subscribe(
            {
            next: (cardPlans) => {
                this.cardPlans = cardPlans
                this.getCardPlansSubject.next(cardPlans)
            },
            error: (e: HttpErrorResponse) => {
                this.getCardPlansSubject.next([])
            }
        })
    }

    public getDictionariesObservable() {
        let subject = new ReplaySubject<Dictionary[]>(1)
        this.replaySubject.subscribe(dictionaries => {
            subject.next(dictionaries)
        })
        return subject;
    }

    public getCardPlansObservable(): Observable<GetCardPlan[]>{
        let subject = new ReplaySubject<GetCardPlan[]>(1)
        this.getCardPlansSubject.subscribe(cardPlans => {
            subject.next(cardPlans)
        })
        return subject;
    }

    getCardPlans(){
        return this.cardPlans
    }

    public createCardPlan(newCardPlan: CreateCardPlan): Observable<any> {
            return this.dataSource.saveData(UserURLs.createCardPlan(),newCardPlan)
        }

        public updateCardPlan(newCardPlan: UpdateCardPlanViewModel): Observable<any> {
            return this.dataSource.updateData(UserURLs.updateCardPlan(),newCardPlan)
        }
}