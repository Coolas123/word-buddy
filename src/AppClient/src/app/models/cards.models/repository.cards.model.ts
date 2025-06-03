import { Injectable } from "@angular/core"
import { CardBox } from "../cardBox.models/cardBox"
import { RestDataSource } from "../rest.datasource"
import { UpdateDictionaryAndRowsCommand } from "../getDictionary.models/getDictionary.model"
import { UserURLs } from "../../common/gateways"
import { Observable, of, ReplaySubject } from "rxjs"
import { HttpErrorResponse } from "@angular/common/http"
import { CardPlanByCardBoxViewModel, UpdateDictionaryRowsLearnStatusCommand } from "./cards.model"

@Injectable({
    providedIn: 'root'
})
export class Model {
    private cardPlan: CardPlanByCardBoxViewModel
    private getDicitonariesIdSubject: ReplaySubject<CardPlanByCardBoxViewModel>
    public  getdictionaryCardPlanIdSubject: ReplaySubject<any>
    private dictionary?: UpdateDictionaryAndRowsCommand
    private replayDictionarySubject: ReplaySubject<UpdateDictionaryAndRowsCommand>
    public currentLearnStatus: string = ""

    constructor(private dataSource: RestDataSource<any>) {
        this.getDicitonariesIdSubject = new ReplaySubject();
        this.cardPlan = new CardPlanByCardBoxViewModel()
        this.getdictionaryCardPlanIdSubject = new ReplaySubject<string>(1)
        this.replayDictionarySubject = new ReplaySubject();

        this.getdictionaryCardPlanIdSubject.subscribe((req)=>{
            this.dataSource.getData(UserURLs.getDictionaryWithRowsByLearnStatus(req.id,req.learnStatus)).subscribe(dictionary => {
                this.dictionary = dictionary
                this.replayDictionarySubject.next(dictionary)
            })
        })
    }

    public loadCardPlan(cardPlanId: string, cardBoxStatus:string){
        this.dataSource.getData(UserURLs.getDictionariesIdByCardPlanIdAndLearnStatus(cardPlanId,cardBoxStatus)).subscribe(
            {
            next: (cardPlanVM) => {
                Object.assign(this.cardPlan,cardPlanVM)
                this.getDicitonariesIdSubject.next(cardPlanVM)
            },
            error: (e: HttpErrorResponse) => {
                this.getDicitonariesIdSubject.next(new CardPlanByCardBoxViewModel())
            }
        })
    }

     public loadDictionaryRows(dictionaryId: string, learnStatus:string){
        this.dataSource.getData(UserURLs.getDictionaryWithRowsByLearnStatus(dictionaryId,learnStatus)).subscribe(
            {
            next: (dictionary) => {
                this.dictionary=dictionary
                this.replayDictionarySubject.next(dictionary)
            },
            error: (e: HttpErrorResponse) => {
                this.replayDictionarySubject.next(new UpdateDictionaryAndRowsCommand())
            }
        })
    }

    public getDictionaryObservable() {
        let subject = new ReplaySubject<UpdateDictionaryAndRowsCommand>(1)
        this.replayDictionarySubject.subscribe(dictionary => {
            subject.next(dictionary)
        })
        return subject;
    }

    public getCardPlanObservable(): Observable<CardPlanByCardBoxViewModel>{
            let subject = new ReplaySubject<CardPlanByCardBoxViewModel>(1)
            this.getDicitonariesIdSubject.subscribe(dictionariesId => {
                subject.next(dictionariesId)
            })
            return subject;
        }

    public serCardBox(cardBox: CardBox) {
        this.cardPlan.CardBox = cardBox
    }

    public setDicitonariesId(dictionariesId: string[]) {
        this.cardPlan.CardBoxDictionariesId = dictionariesId
    }

    public getCardBox(): CardBox | undefined {
        return this.cardPlan.CardBox ?? undefined
    }

    public getCardPlanDicitonariesId(): string[]  {
        return this.cardPlan.CardBoxDictionariesId!
    }

    public getCardPlan(): CardPlanByCardBoxViewModel  {
        return this.cardPlan
    }

    public getDicitonary(): UpdateDictionaryAndRowsCommand | undefined  {
        return this.dictionary
    }

    public updateLearnStatusDictionaryRows(dictionaryRows:UpdateDictionaryRowsLearnStatusCommand){
        if(dictionaryRows.DictionaryRows.length == 0) return of(null)
        return this.dataSource.updateData(UserURLs.updateDictionaryRows(),dictionaryRows);
    }
}