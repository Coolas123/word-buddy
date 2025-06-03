import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Observable, of } from "rxjs";
import { Injectable } from "@angular/core";
import { Model } from "./repository.cards.model";
import { CardPlanByCardBoxViewModel } from "./cards.model";
import { UpdateDictionaryAndRowsCommand } from "../getDictionary.models/getDictionary.model";


@Injectable({
    providedIn: 'root'
})
export class GetCardsResolver implements Resolve<any> {
    constructor(private dataSource: RestDataSource<any>, private model: Model) {
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
        if (route.params["cardMode"] == "test") {
            let cardBoxStatus = route.params["cardBoxStatus"]
            let cardPlanId = route.params["cardPlanId"]
            if (this.model.getCardPlanDicitonariesId() == undefined || this.model.getCardPlanDicitonariesId()?.length == 0 || cardPlanId != this.model.getCardPlan().CardPlanId || cardBoxStatus != this.model.currentLearnStatus) {
                this.model.currentLearnStatus = cardBoxStatus
                this.model.loadCardPlan(cardPlanId, cardBoxStatus)
                return this.model.getCardPlanObservable()
            }
            else{
                return of(new CardPlanByCardBoxViewModel())
            }
        }
        else {
            let learnStatus = route.params["learnStatus"]
            let dictionaryId = route.params["dictionaryId"]
            if (this.model.getDicitonary() == undefined || dictionaryId != this.model.getDicitonary()?.UpdateDictionaryCommand?.Id || learnStatus != this.model.currentLearnStatus) {
                this.model.currentLearnStatus = learnStatus
                this.model.loadDictionaryRows(dictionaryId, learnStatus)
                return this.model.getDictionaryObservable()
            }
            else{
                return of(new UpdateDictionaryAndRowsCommand())
            }
        }
        
    }
}