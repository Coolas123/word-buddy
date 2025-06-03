import { Injectable } from "@angular/core"
import { UserURLs } from "../../common/gateways"
import { firstValueFrom, Observable, ReplaySubject } from "rxjs"
import { UpdateDictionaryAndRowsCommand, UpdateDictionaryRowCommand } from "../getDictionary.models/getDictionary.model"
import { RestDataSource } from "../rest.datasource"

@Injectable({
    providedIn: 'root'
})
export class Model {
    public dictionariesCardId: string[] = []

    constructor(private dataSource:RestDataSource<UpdateDictionaryAndRowsCommand>) {
        
    }

    public updateDictionary(newDictionary: UpdateDictionaryAndRowsCommand): Observable<any> {
            return this.dataSource.updateData(UserURLs.updateDictionaryURI(), newDictionary)
        }

    // public getDictionaryObservable() {
    //         let subject = new ReplaySubject<UpdateDictionaryAndRowsCommand>(1)
    //         this.replaySubject.subscribe(dictionary => {
    //             subject.next(dictionary)
    //         })
    //         return subject;
    //     }
}