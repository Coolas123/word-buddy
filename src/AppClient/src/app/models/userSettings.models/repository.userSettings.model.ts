import { Injectable } from "@angular/core"
import { UserSettings } from "./userSettings.model"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { BehaviorSubject, Observable, of, ReplaySubject } from "rxjs"

@Injectable({
    providedIn: 'root'
  })
export class Model {
    public userSettings: UserSettings = new UserSettings()
    private replaySubject: ReplaySubject<UserSettings>

    constructor(public dataSource: RestDataSource<UserSettings>) {
        this.replaySubject = new ReplaySubject<UserSettings>(1)
        this.dataSource.getData(UserURLs.settingsURI()).subscribe(settings=>{
            this.userSettings = settings
            this.replaySubject.next(settings)
        })
    }

    public saveSettings(newSettings: UserSettings): Observable<any> {
        return this.dataSource.updateData(UserURLs.settingsURI(), newSettings)
    }

    public getSettings(): UserSettings {
        return this.userSettings
    }

    public getSettingsObservable(): Observable<UserSettings> {
        let subject = new ReplaySubject<UserSettings>(1)
        this.replaySubject.subscribe(settings=>{
            subject.next(settings)
        })
        return subject
    }
}