import { Injectable } from "@angular/core"
import { RegisterUser } from "./registerUser.model"
import { RestDataSource } from "../rest.datasource"
import { UserURLs } from "../../common/gateways"
import { Observable } from "rxjs"
import { LoginUser } from "../loginUser.models/loginUser.model"

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
}