import { isPlatformBrowser } from "@angular/common";
import { Inject, Injectable,PLATFORM_ID } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { LocalStorageService } from "./LocalStorageService";
import { JwtHelperService } from "@auth0/angular-jwt"



@Injectable({
    providedIn: 'root'
})
export class TokenService{
    private _isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)

    constructor(private localStorageService:LocalStorageService,private jwtHelper: JwtHelperService){
        const token = this.getToken()
        if(token)
            this.updateToken(true)
    }

    get isAuthenticated(){
        return this._isAuthenticated
    }

    updateToken(status: boolean){
        this._isAuthenticated.next(status)
    }

    setToken(token: string){
        this.updateToken(true)
        this.localStorageService.setItem("jwt",token)
        
    }

    getToken(){
        return this.localStorageService.getItem("jwt")
    }

    removeToken(){
        this.updateToken(false)
        return this.localStorageService.removeItem("jwt")
    }
}