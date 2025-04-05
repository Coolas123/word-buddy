import { isPlatformBrowser } from "@angular/common";
import { Inject, Injectable,PLATFORM_ID } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { LocalStorageService } from "./LocalStorageService";



@Injectable({
    providedIn: 'root'
})
export class TokenService{
    public isAuthenticated: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)

    constructor(private localStorageService:LocalStorageService){
        const token = this.getToken()
        if(token){
            this.updateToken(true)
        }
    }

    updateToken(status: boolean){
        this.isAuthenticated.next(status)
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