import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { GetCardPlan } from "./CardPlan.model";
import { Model } from "./repository.getDictionariesFreeForCardPlan";

@Injectable({
    providedIn: 'root'
  })
export class CardPlansResolver implements Resolve<GetCardPlan[]>{
    constructor(private dataSource: RestDataSource<GetCardPlan[]>, private model:Model){
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<GetCardPlan[]>{
        this.model.loadCardPlans()
        return this.model.getCardPlansObservable()
    }
}