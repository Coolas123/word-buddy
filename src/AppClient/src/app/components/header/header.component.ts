import { Component } from "@angular/core";
import { TokenService } from "../../models/token.service";
import { NavigationEnd, Router, RouterModule } from "@angular/router";
import { UserURLs } from "../../common/gateways";
import { Model } from "../../models/registerUser.models/repository.User.model";
import { MessageService } from "../../models/alertMessage.models/alertMessage.service";
import { filter } from "rxjs";
import { Message } from "../../models/alertMessage.models/message.model";


@Component({
    selector: "headerComponent",
    templateUrl: "./header.component.html",
    imports: [RouterModule],
    providers: [Model]
})
export class HeaderComponent{
    public isAuthenticated: boolean = false

    constructor(private router: Router, private model: Model, private messageService: MessageService)
    {
        this.model.isAuthrnticatedBehaviorSubject().subscribe(x=>{
            if(!x){
                router.navigateByUrl(UserURLs.loginURI())
                this.isAuthenticated = false
            }
            else {
                this.isAuthenticated = true
            }
        })
    }

    public logout() {
        this.model.logout()
        this.router.navigateByUrl("/")
        this.router.events.pipe(
            filter((event: any) => event instanceof NavigationEnd)
        ).subscribe(_ => this.messageService.reportMessage(new Message("Вы успешно вышли из учетной записи")));
    }
}