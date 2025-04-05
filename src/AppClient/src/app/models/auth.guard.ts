import { inject, Injectable } from "@angular/core";
import { UserURLs } from "../common/gateways"
import {
    ActivatedRouteSnapshot, RouterStateSnapshot,
    Router,
    CanActivate,
    CanActivateFn,
    GuardResult,
    MaybeAsync
} from "@angular/router";
//import { AuthService } from "./auth.service";
import { TokenService } from "./token.service";

export const AuthGuard: CanActivateFn = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) => {
    let router = inject(Router)
    let auth = inject(TokenService)
    auth.isAuthenticated.subscribe({
        next:(value)=>{
            if(!value){
                router.navigateByUrl(UserURLs.loginURI());
            }
        }
    })
    return true;
}