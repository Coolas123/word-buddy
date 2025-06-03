// import { Injectable } from "@angular/core"
// import { RestDataSource } from "../rest.datasource"
// import { UserURLs } from "../../common/gateways"
// import { Observable } from "rxjs/internal/Observable"
// import { ReplaySubject } from "rxjs"
// import { HttpErrorResponse } from "@angular/common/http"
// import { MessageService } from "../alertMessage.models/alertMessage.service"
// import { Message } from "../alertMessage.models/message.model"
// import { GetCardPlanWithDictionaries } from "../cardPlan.models/CardPlan.model"

// @Injectable({
//     providedIn: 'root'
// })
// export class Model {
//     cardPlan?: GetCardPlanWithDictionaries = undefined
//     private replaySubject: ReplaySubject<GetCardPlanWithDictionaries>
//     public getCardPlanIdSubject: ReplaySubject<string>

//     constructor(public dataSource: RestDataSource<GetCardPlanWithDictionaries>, private messageService: MessageService) {
//         this.replaySubject = new ReplaySubject<GetCardPlanWithDictionaries>(1)
//         this.getCardPlanIdSubject = new ReplaySubject<string>(1)

//         this.getCardPlanIdSubject.subscribe(id => {
//             this.dataSource.getData(UserURLs.getCardPlanWithDictionaries(id)).subscribe(
//                 {
//                     next: (cardPlan) => {
//                         this.cardPlan = cardPlan
//                         this.replaySubject.next(cardPlan)
//                     }
//                 })
//         })
//     }

//     public getCardPlan(): GetCardPlanWithDictionaries | undefined {
//         return this.cardPlan ?? undefined
//     }

//     public getCardPlanObservable(): Observable<GetCardPlanWithDictionaries> {
//         let subject = new ReplaySubject<GetCardPlanWithDictionaries>(1)
//         this.replaySubject.subscribe(cardPlan => {
//             subject.next(cardPlan)
//         })
//         return subject
//     }
// }