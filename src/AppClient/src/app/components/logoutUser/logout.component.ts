import { Component } from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { MessageService } from "../../models/alertMessage.models/alertMessage.service";


@Component({
    selector: "logout",
    templateUrl: "./logout.component.html",
    imports: [RouterModule],
})
export class LogoutComponent{
    constructor(private router: Router, private messageService: MessageService){
    }

    
}