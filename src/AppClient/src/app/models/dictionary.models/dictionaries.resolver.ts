import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Dictionary } from "./dictionary.model";
import { Model } from "./repository.dictionaries.model";
import { Observable, of } from "rxjs";
import { Injectable } from "@angular/core";
import { Model as DictionariesModel } from "../../models/dictionary.models/repository.dictionaries.model"

@Injectable({
    providedIn: 'root'
  })
export class DcitionariesResolver implements Resolve<Dictionary[]>{
    constructor(private dataSource: RestDataSource<Dictionary[]>, private model:Model){
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<Dictionary[]>{
        if(this.model.dictionaries.length == 0){
            this.model.loadDIctionaries()
        }
        return this.model.getDictionariesObservable()
    }
}