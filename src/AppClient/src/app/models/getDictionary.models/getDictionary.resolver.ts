import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Model } from "./repository.getDictionary.model";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { UpdateDictionaryAndRowsCommand } from "./getDictionary.model";

@Injectable({
    providedIn: 'root'
  })
export class GetDcitionaryResolver implements Resolve<UpdateDictionaryAndRowsCommand>{
    constructor(private dataSource: RestDataSource<UpdateDictionaryAndRowsCommand>, private model:Model){
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<UpdateDictionaryAndRowsCommand>{
        this.model.dictionaryIdSubject.next(route.params["id"])
        return this.model.getDictionaryObservable()
    }
}