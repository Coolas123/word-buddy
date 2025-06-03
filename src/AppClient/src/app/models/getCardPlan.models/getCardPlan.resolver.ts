// import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
// import { RestDataSource } from "../rest.datasource";
// import { Observable, of } from "rxjs";
// import { Injectable } from "@angular/core";
// import { Model } from "./repository.getCardPlan.model"
// import { GetCardPlanWithDictionaries } from "../cardPlan.models/CardPlan.model";

// @Injectable({
//     providedIn: 'root'
//   })
// export class GetCardPlanResolver implements Resolve<GetCardPlanWithDictionaries>{
//     constructor(private dataSource: RestDataSource<GetCardPlanWithDictionaries>, private model:Model){
//     }
//     resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<GetCardPlanWithDictionaries>{
//         this.model.getCardPlanIdSubject.next(route.params["id"])
//         return this.model.getCardPlanObservable()
//     }
// }