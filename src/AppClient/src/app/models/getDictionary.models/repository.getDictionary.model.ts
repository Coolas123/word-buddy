import { Inject, Injectable } from "@angular/core"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable } from "rxjs/internal/Observable"
import { UpdateDictionaryAndRowsCommand } from "./getDictionary.model"
import { ReplaySubject } from "rxjs"
import { MoveToAnotherDicitonaryRowsCommand, MoveToAnotherDictionary } from "./moveToAnotherDictionary.model"

@Injectable({
    providedIn: 'root'
})
export class Model {
    public dictionary: UpdateDictionaryAndRowsCommand = new UpdateDictionaryAndRowsCommand()
    public dictionaryIdSubject: ReplaySubject<string>
    private replaySubject: ReplaySubject<UpdateDictionaryAndRowsCommand>

    constructor(public dataSource: RestDataSource<UpdateDictionaryAndRowsCommand>,public dataSource1: RestDataSource<MoveToAnotherDicitonaryRowsCommand>) {
        this.replaySubject = new ReplaySubject<UpdateDictionaryAndRowsCommand>(1)
        this.dictionaryIdSubject = new ReplaySubject<string>(1)

        this.dictionaryIdSubject.subscribe(id=>{
            this.dataSource.getData(UserURLs.getDictionaryURI(id)).subscribe(dictionary => {
                this.dictionary = dictionary
                this.replaySubject.next(dictionary)
            })
        })
    }

    public updateDictionary(newDictionary: any): Observable<any> {
        return this.dataSource.updateData(UserURLs.updateDictionaryURI(), newDictionary)
    }

    public moveToAnotherDictionaryDictionary(moveToAnotherDictionary: MoveToAnotherDicitonaryRowsCommand): Observable<any> {
        return this.dataSource1.updateData(UserURLs.moveToAnotherDicitonary(), moveToAnotherDictionary)
    }

    public getDictionaryObservable() {
        let subject = new ReplaySubject<UpdateDictionaryAndRowsCommand>(1)
        this.replaySubject.subscribe(dictionary => {
            subject.next(dictionary)
        })
        return subject;
    }
}