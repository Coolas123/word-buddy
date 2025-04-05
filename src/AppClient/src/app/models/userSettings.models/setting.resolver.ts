import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { Model } from "./repository.userSettings.model"
import { RestDataSource } from "../rest.datasource";
import { UserSettings } from "./userSettings.model"
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
  })
export class GetSettingsResolver implements Resolve<UserSettings>{
    constructor(private dataSource: RestDataSource<UserSettings>, private model:Model){
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):Observable<UserSettings>{
        return this.model.getSettingsObservable()
    }
}