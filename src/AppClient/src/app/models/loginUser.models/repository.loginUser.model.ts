import {Injectable} from "@angular/core"
import {LoginUser} from "./loginUser.model"
import {RestDataSource} from "../rest.datasource"
import {UserURLs} from "../../common/gateways"
import { Observable } from "rxjs"

@Injectable()
export class Model{
    constructor(public dataSource: RestDataSource<any>){

    }

    public authenticate(loginUser:LoginUser):Observable<any>{
        return this.dataSource.authenticate(loginUser)
    }
}