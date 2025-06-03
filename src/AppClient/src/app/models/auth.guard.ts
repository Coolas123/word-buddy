import { inject } from "@angular/core";
import { UserURLs } from "../common/gateways"
import {
    ActivatedRouteSnapshot, RouterStateSnapshot,
    Router,
    CanActivateFn,
} from "@angular/router";
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