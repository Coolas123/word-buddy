import { Injectable } from "@angular/core"
import { RegisterUser } from "./registerUser.model"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { BehaviorSubject, Observable } from "rxjs"

@Injectable()
export class Model {
    constructor(private dataSource: RestDataSource<RegisterUser>) {

    }

    public saveUser(newUser: RegisterUser): Observable<any> {
        return this.dataSource.saveData(UserURLs.registerURI(), newUser)
    }

    public authenticate(token: string): void {
        this.dataSource.tokenService.setToken(token)
    }

    public logout(): void{
        this.dataSource.tokenService.removeToken()
    }

    public isAuthrnticatedBehaviorSubject():BehaviorSubject<boolean> {
        return this.dataSource.tokenService.isAuthenticated
    }
}