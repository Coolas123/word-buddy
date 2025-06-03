import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { RestDataSource } from "../rest.datasource";
import { Observable, of } from "rxjs";
import { Injectable } from "@angular/core";
import { Model } from "./repository.model";

@Injectable({
    providedIn: 'root'
})
export class aiResolver implements Resolve<any> {
    constructor(private dataSource: RestDataSource<any>, private model: Model) {
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {

        return this.model.getDictionariesObservable()
    }
}