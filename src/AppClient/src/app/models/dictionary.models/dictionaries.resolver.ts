import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Dictionary } from "./dictionary.model";
import { Model } from "./repository.dictionaries.model";
import { Observable, of } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
export class DcitionariesResolver implements Resolve<Dictionary[]>{
    constructor(private dataSource: RestDataSource<Dictionary[]>, private model:Model){
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<Dictionary[]>{
        return this.model.getDictionariesObservable()
    }
}