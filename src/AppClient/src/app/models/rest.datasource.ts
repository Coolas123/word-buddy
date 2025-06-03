import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse, } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { Observable, throwError, } from "rxjs"
import { catchError } from "rxjs/operators"
import { UserURLs } from "../common/gateways"
import { map } from 'rxjs';
import { LoginUser } from "./loginUser.models/loginUser.model";
import { TokenService } from "./token.service";

@Injectable({
    providedIn: 'root'
})
export class RestDataSource<T> {
    constructor(public tokenService: TokenService, private http: HttpClient) { }

    authenticate(loginUser: LoginUser): Observable<any> {
        return this.http.post<any>(UserURLs.loginURI(), loginUser).pipe(map(response => {
            if (response) {
                this.tokenService.setToken(response.jwt)
            }
            return response;
        }));
    }

    getData(url: string): Observable<T> {
        return this.sendRequest("GET", url)
    }

    saveData(url: string, body: T): Observable<T> {
        return this.sendRequest("POST", url, body)
    }

    updateData(url: string, body: T): Observable<T> {
        return this.sendRequest("PATCH", url, body)
    }

    private sendRequest(httpMethod: string, url: string, body?: T): Observable<T> {
        let options: {
            body: T | undefined,
            headers?: HttpHeaders,
        }
        options = {
            body: body
        }

        if (this.tokenService.getToken() != null) {
            let headersToSend = new HttpHeaders().set("Authorization", `Bearer ${this.tokenService.getToken()}`)
            options = {
                body: body,
                headers: headersToSend
            }
        }
        return this.http.request<T>(httpMethod, url, options).pipe(
            catchError((err: any) => {
                if (err.status == "401") {
                    this.tokenService.removeToken()
                }
                return throwError(() => err)
            }))

        // res.subscribe({
        //     error:(error)=>{
        //         if(error.status == "401"){
        //             this.tokenService.removeToken()
        //         }
        //     }
        // })
    }
}