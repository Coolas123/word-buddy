import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RestDataSource } from "./rest.datasource";
import { LoginUser } from "./loginUser.models/loginUser.model";
import { TokenService } from "./token.service";
import { HttpClient } from "@angular/common/http";
import { UserURLs } from "../common/gateways";
import { map } from 'rxjs';


/*export class AuthService {
    constructor(public tokenService: TokenService, private http: HttpClient) { }

    onLogin(loginUser: LoginUser) {
        return this.http.post<any>(UserURLs.loginURI(), loginUser).pipe(map(response => {
            if (response) {
                this.tokenService.setToken(response.jwt)
            }
            return response;
        }));
    }
}*/

/*
@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(public tokenService: TokenService, public datasource: RestDataSource<any>) { }

    authenticate(loginUser: LoginUser) {
        return this.datasource.authenticate(loginUser);
    }

    get authenticated(): boolean {
        console.log(this.datasource.authToken + " gdfgdf")
        return this.datasource.authToken != null;
    }
}*/
